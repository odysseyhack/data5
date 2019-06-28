﻿using Dapper;
using DatasetDownloader.DataContracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DatasetDownloader.DataAccess
{
    public class MetaDataAccess : IMetaDataAccess
    {
        private int odsid { get; set; }
        private SqlConnection connection { get; set; }
        string InsertLineData = "INSERT INTO [dbo].[odsmetadata2]" + "([ods_id]" + ",[FieldIndex]" + ",[FieldName]" + ",[AverageFieldLength]" + ",[MaximumFieldLength]" + ",[MinimumFieldLength]" +
            ",[FieldType]" + ",[FieldConsistenceList]" + ",[FieldIsConsistent]" + ",[ConsistentDataType]" + ",[DamerauValue])" + "VALUES" + "({0}" +
            ",{1}" + ",'{2}'" + ",{3}" + ",{4}" + ",{5}" + ",'{6}'" + ",'{7}'" + ",'{8}'" + ",'{9}'" + ",{10})";

        string InsertHeaderData = "INSERT INTO[dbo].[opendatasource]" + "([filename]" + ",[bron])" + "VALUES" + "('{0}'" + ",'{1}')";

        public bool InsertDataFieldData(DataFieldMain Datafield, string databaseConnection)
        {
            try
            {
                using (connection = new SqlConnection(databaseConnection))
                {
                    var data = new string[]
                        {
                            Datafield.CleansetFilename,
                            "http://www.TheInternets.com"
                        };

                    ExecuteMainInsertMethod(Datafield, databaseConnection, data);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                this.connection.Close();
            }

            return true;
        }

        private void ExecuteMainInsertMethod(DataFieldMain Datafield, string databaseConnection, string[] data)
        {
            Console.WriteLine(DateTime.Now.ToString() + " - Writing header to database for file: " + Datafield.CleansetFilename);
            var sql = string.Format(InsertHeaderData, data);
            this.connection.Execute(sql);
            this.odsid = this.GetMaxOds(databaseConnection);
            this.InsertData(Datafield, databaseConnection);
        }

        private bool InsertData(DataFieldMain Datafield, string databaseConnection)
        {
            var counter = 0;
            try
            {
                using (this.connection = new SqlConnection(databaseConnection))
                {
                    foreach (var item in Datafield.DataFieldAnalysis)
                    {
                        string[] data = CreateOdString(counter, item);
                        counter = ExecuteInsertMethod(counter, connection, item, data);
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                this.connection.Close();
            }

            return true;
        }

        private int ExecuteInsertMethod(int counter, SqlConnection connection, DataFieldAnalysis item, string[] data)
        {
            var sql = string.Format(InsertLineData, data);
            counter += 1;
            Console.WriteLine(DateTime.Now.ToString() + " - Writing line data to database for field: " + item.FieldName + " - " + item.ConsistentDataType);
            connection.Execute(sql);
            return counter;
        }

        private string[] CreateOdString(int counter, DataFieldAnalysis item)
        {
            return new string[] {
                            this.odsid.ToString(),
                            counter.ToString(),
                            item.FieldName,
                            item.AverageFieldLength.ToString(),
                            item.MaximumFieldLength.ToString(),
                            item.MinimumFieldLength.ToString(),
                            item.FieldType.First().ToString(),
                            item.FieldConsistenceList.First().ToString().Replace(";",string.Empty),
                            item.FieldIsConsistent ? "J" : "N",
                            item.ConsistentDataType.Replace(";",string.Empty),
                            item.DamerauValue.ToString()
                        };
        }

        public int GetMaxOds(string databaseConnection)
        {
            using (var connection = new SqlConnection(databaseConnection))
            {
                try
                {
                    Console.WriteLine(DateTime.Now.ToString() + " - Getting automated generated Id for storage!");
                    var metaData = connection.Query<OpenMainData>("SELECT max(id) as id from [opendatasource]").ToList();
                    return metaData != null && metaData.Any() ? metaData.First().id : 0;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return 0;
            }
        }
    }
}