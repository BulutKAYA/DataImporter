using DataImporter.DataBaseMameger;
using DataImporter.DatabaseRepository.EfRepository;
using DataImporter.Entityes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DataImporter.XmlImport
{
    public class XmlImporter : IXmlImporter
    {
        string supplierUrl = "";
        
        IStockManeger stockManeger;
        IProductManeger productManeger;

        DataTable dtProduct;
        DataTable dtStock;
        
        DataTable dtConfigTables;
        DataTable dtConfigFields;
        public XmlImporter(string _supplierUrl, string _configurationFilePath)
        {
            supplierUrl = _supplierUrl;
            
            stockManeger = new StockManeger(new StockRepository());
            productManeger = new ProductManeger(new ProductRepository());
            LoadConfigurationTables(_configurationFilePath);
        }
        
        private void LoadConfigurationTables(string configurationFilePath)
        {
            dtProduct = new DataTable();
            dtProduct.Columns.Add("ProductId", typeof(string));
            dtProduct.Columns.Add("ProductName", typeof(string));

            dtStock = new DataTable();
            dtStock.Columns.Add("ProductName", typeof(string));
            dtStock.Columns.Add("Barcode", typeof(string));
            dtStock.Columns.Add("Piece", typeof(string));

            dtConfigTables = new DataTable();
            dtConfigTables.Columns.Add("id", typeof(string));
            dtConfigTables.Columns.Add("parentid", typeof(string));
            dtConfigTables.Columns.Add("sourcetablename", typeof(string));
            dtConfigTables.Columns.Add("destinationtablename", typeof(string));
            dtConfigTables.Columns.Add("seperatername", typeof(string));

            dtConfigFields = new DataTable();
            dtConfigFields.Columns.Add("parenttableid", typeof(string));
            dtConfigFields.Columns.Add("sourcefieldname", typeof(string));
            dtConfigFields.Columns.Add("destinationfieldname", typeof(string));

            XmlTextReader reader = new XmlTextReader(configurationFilePath);
            DataRow tableDataRow = null;
            DataRow fieldDataRow = null;
            string parentTableId = "";

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "table":
                            tableDataRow = dtConfigTables.NewRow();
                            tableDataRow["id"] = reader.GetAttribute("id");
                            tableDataRow["parentid"] = parentTableId;
                            parentTableId = reader.GetAttribute("id");
                            dtConfigTables.Rows.Add(tableDataRow);
                            break;
                        case "sourcetablename":
                            tableDataRow["sourcetablename"] = reader.ReadElementString();
                            break;
                        case "destinationtablename":
                            tableDataRow["destinationtablename"] = reader.ReadElementString();
                            break;
                        case "seperatername":
                            tableDataRow["seperatername"] = reader.ReadElementString();
                            break;
                        case "field":
                            fieldDataRow = dtConfigFields.NewRow();
                            fieldDataRow["parenttableid"] = reader.GetAttribute("tableid");
                            dtConfigFields.Rows.Add(fieldDataRow);
                            break;
                        case "sourcefieldname":
                            fieldDataRow["sourcefieldname"] = reader.ReadElementString();
                            break;
                        case "destinationfieldname":
                            fieldDataRow["destinationfieldname"] = reader.ReadElementString();
                            break;
                    }
                }
            }
        }
        
        private DataRow GetElementFieldConfigRow(string name)
        {
            if (dtConfigFields.Select("sourcefieldname=" + "'" + name + "'").Length > 0)
                return dtConfigFields.Select("sourcefieldname=" + "'" + name + "'")[0];
            else
                return null;
        }
        
        public string LoadData()
        {
            List<string> fieldsToUpdateDestination =
                new List<string> { "ProductName", "Barcode", "Piece" };//burası güncellenmesini istediğimiz alanlar
            
            string productName = "";
            string speraterName = "";
            DataRow rEntity = null;
            DataRow rTable = null;
            
            XmlTextReader readerx = new XmlTextReader(supplierUrl);
            XDocument xDocument = XDocument.Load(readerx);
           

            foreach (XElement element in xDocument.Descendants())
            {
                if (element.NodeType == XmlNodeType.Element)
                {
                    string elementName = element.Name.ToString();
                    
                    if (dtConfigTables.Select("seperatername = " + "'" + elementName + "'").Length > 0)
                    {
                        speraterName = elementName;
                        rTable = dtConfigTables.Select("seperatername = " + "'" + elementName + "'")[0];
                    }

                    if (rTable != null && elementName == speraterName)
                    {
                        switch (rTable["destinationtablename"].ToString())//daha başka tablolarında okunabilmesi için böyle yapıldı
                        {
                            case "Product":
                                rEntity = dtProduct.NewRow();
                                dtProduct.Rows.Add(rEntity);
                                break;
                            case "Stock":
                                rEntity = dtStock.NewRow();
                                dtStock.Rows.Add(rEntity);
                                break;
                        }
                    }

                    DataRow rField = GetElementFieldConfigRow(elementName);
                    if (rField != null && rEntity != null)
                    {
                        string destinationFieldName = rField["destinationfieldname"].ToString();

                        if (fieldsToUpdateDestination.Contains(destinationFieldName))
                        {
                            string result = element.Value;
                            rEntity[destinationFieldName] = result;//burası sadece bu iki tabloya göre özelleştirildi ama daha dinamik bi yapıya çevlilebilinir
                            if (rTable["destinationtablename"].ToString() == "Product" &&
                                destinationFieldName == "ProductName")
                            {
                                productName = result;
                            }
                            else if (rTable["destinationtablename"].ToString() == "Stock")
                            {
                                rEntity["ProductName"] = productName;
                            }
                        }
                    }
                }
            }
            
            return xDocument.ToString();
        }
        
        public bool SaveData()
        {
            foreach(DataRow rStock in dtStock.Select())
            {
                Product product = productManeger.Get(rStock["productName"].ToString());
                if(product != null)
                {
                    string url = productManeger.GetSupplierUrl(product.ProductName);
                    
                    if (url == supplierUrl)//burda bir ürünün bir tedarikci tarafından güncellenmesini garanti altına almış olduk
                    {
                        Stock stock = stockManeger.Get(rStock["Barcode"].ToString());
                        if(stock != null)
                        {
                            stock.Piece = Convert.ToInt32(rStock["Piece"].ToString());
                            stockManeger.Update(stock);
                        }
                    }
                }
            }//aynı şeyleri product tablosu içinde yapabilirdik
            return true;
        }
        public bool Validate()
        {//datalarlar aktarılmadan önce yapılması gereken kontroller burada yapılabilinir
            return true;
        }
    }
}
