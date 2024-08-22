using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Response;
using Common.Settings;
using Domain;
using Infraestructura;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class FoodService
    {
        private readonly IConfigurationLib config;
        private readonly ClientDbContext context;

        public FoodService(IConfigurationLib config)
        {
            this.config = config;
            
        }


        public EResponseBase<Food> Get()
        {


            EResponseBase<Food> response = new EResponseBase<Food>
            {
                Code = 200,
                Message = "OK",
                listado = new List<Food>()
            };
            try
            {

                using (SqlConnection connection = new SqlConnection(config.ConnectionString))
                {
                    connection.Open();


                    string sqlQuery = @"
                    SELECT 
                        f.FoodName,
	                    f.TypeProduct,
	                    f.Quantity,
	                    f.ImagePath,
	                    fc.FoodClassificationName,
	                    fsc.FoodSubClassificationName
                    FROM 
                        Foods f
                    LEFT JOIN 
                        FoodClassifications fc ON f.FoodClassificationId = fc.FoodClassificationId
                    LEFT JOIN 
                        FoodSubClassifications fsc ON f.FoodSubClassificationId = fsc.FoodSubClassificationId
                    LEFT JOIN 
                        FoodSubClassificationCodes fscc ON f.FoodSubClassificationCodeId = fscc.FoodSubClassificationCodeId
                    LEFT JOIN 
                        ClassificationRules cr ON f.ClassificationRuleId = cr.ClassificationRuleId
                    LEFT JOIN 
                        TypeProducts tp ON f.TypeProductId = tp.TypeProductId
                    LEFT JOIN 
                        FoodPackages fp ON f.FoodPackageId = fp.FoodPackageId
                    LEFT JOIN 
                        FoodVarieties fv ON f.FoodVarietyId = fv.FoodVarietyId
                    LEFT JOIN 
                        Factories fac ON f.FactoryId = fac.FactoryId
                    LEFT JOIN 
                        Measurements m ON f.MeasurementId = m.MeasurementId
                    WHERE 
                        f.Enabled = 1
                    ORDER BY 
                        fc.Orden, 
                        fsc.Orden, 
                        f.VarietyOrder;
                    ";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var list = new List<Food>();

                            while (reader.Read())
                            {
                                Food food = new Food
                                {
                                    FoodName = reader["FoodName"]?.ToString() ?? string.Empty,
                                    TypeProduct = reader["TypeProduct"]?.ToString() ?? string.Empty,
                                    Quantity = reader["Quantity"] != DBNull.Value ? Convert.ToInt32(reader["Quantity"]) : 0,
                                    ImagePath = reader["ImagePath"]?.ToString() ?? string.Empty,
                                    FoodClassificationName = reader["FoodClassificationName"]?.ToString() ?? string.Empty,
                                    FoodSubClassificationName = reader["FoodSubClassificationName"]?.ToString() ?? string.Empty
                                };
                                list.Add(food);
                            }

                            response.listado = list;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                response.Code = 500;
                response.Message = e.Message;
            }

            return response;
        }


        public EResponseBase<FoodClassification> GetWithSubClassification()
        {
            EResponseBase<FoodClassification> response = new EResponseBase<FoodClassification>
            {
                Code = 200,
                Message = "OK",
                listado = new List<FoodClassification>()
            };
            try
            {

                using (SqlConnection connection = new SqlConnection(config.ConnectionString))
                {
                    connection.Open();


                    string sqlQuery = @"
                    SELECT fc.FoodClassificationName, fsc.FoodSubClassificationName
                    FROM FoodClassifications fc
                    LEFT JOIN FoodSubClassifications fsc ON fc.FoodClassificationID = fsc.FoodClassificationID
                    WHERE fc.Enabled = 1
                    ORDER BY fc.Orden;
                    ";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var foodclassification = new FoodClassification();

                            List<FoodClassification> list = new List<FoodClassification>();

                            string currentClassification = string.Empty;

                            while (reader.Read())
                            {
                                 if(currentClassification != reader["FoodClassificationName"]?.ToString() )
                                 {
                                    foodclassification = new FoodClassification
                                    {
                                        FoodClassificationName = reader["FoodClassificationName"]?.ToString() ?? string.Empty,
                                        FoodSubClassifications = new List<FoodSubClassification>()
                                    };

                                }


                                var subClassification = new FoodSubClassification
                                {
                                    FoodSubClassificationName = reader["FoodSubClassificationName"]?.ToString() ?? string.Empty,

                                };

                                if (currentClassification != reader["FoodClassificationName"]?.ToString())
                                {

                                    list.Add(foodclassification);
                                }

                                currentClassification = reader["FoodClassificationName"]?.ToString() ?? string.Empty;

                                foodclassification.FoodSubClassifications.Add(subClassification);
                            }

                            response.listado = list;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                response.Code = 500;
                response.Message = e.Message;
            }

            return response;
        }
    }
}
