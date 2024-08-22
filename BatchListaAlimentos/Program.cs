using Common.Settings;
using Common.Response;
using Domain;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Common.Settings;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Services;
using SkiaSharp;

public class Program
{

    public static IConfigurationLib Configuration { get; set; }

    public static FoodService foodService;

    static async Task Main()
    {
        var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

        Configuration = new ConfigurationLib(builder);



        Console.WriteLine("Iniciando programa");

        try
        {

            GenerateFoodList();
            GenerateIndexList();
            GeneratePDF();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }



        //Console.WriteLine("Presiona Enter para salir...");
        //Console.ReadLine(); // Pausa para evitar que el programa se cierre inmediatamente




    }

    static void GenerateFoodList()
    {
        foodService = new FoodService(Configuration);

        EResponseBase<Food> list = foodService.Get();

        // Tomar solo los primeros 15 registros
        System.Collections.Generic.List<Food> foodList = list.listado.ToList();

        var directoryPath = Path.Combine(Configuration.FilesPath);
        var filePath = Path.Combine(directoryPath, $"Alimentosxd.pdf");

        // Crear el directorio si no existe
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            // Convertir el color hexadecimal a BaseColor
            BaseColor textColor = new BaseColor(0x13, 0xA8, 0x9C); // Color #13A89C

            // Crear un Font con el color deseado
            iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, textColor);
            iTextSharp.text.Font categoryFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, textColor);
            iTextSharp.text.Font subcategoryFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD, textColor);

            // Tabla principal con 3 columnas
            PdfPTable mainTable = new PdfPTable(3);
            mainTable.WidthPercentage = 100; // Ajustar el ancho de la tabla al tamaño de la página

            int count = 0; // Contador para controlar el número de registros agregados
            int categoryNumber = 0; // Inicializar el contador de categorías
            string currentCategory = null;
            string currentSubcategory = null;

            foreach (var item in foodList)
            {
                // Verificar si cambió la categoría
                if (item.FoodClassificationName != currentCategory)
                {
                    if (currentCategory != null)
                    {
                        // Añadir la tabla principal al documento y comenzar una nueva página
                        document.Add(mainTable);
                        document.NewPage();
                        mainTable = new PdfPTable(3);
                        mainTable.WidthPercentage = 100;
                    }

                    currentCategory = item.FoodClassificationName;
                    categoryNumber++; // Incrementar el contador de categorías

                    // Dibujar el círculo y el número
                    PdfContentByte cb = writer.DirectContent;
                    cb.BeginText();
                    cb.SetFontAndSize(BaseFont.CreateFont(), 12);
                    float centerX = 40; // Ajustar según sea necesario
                    float centerY = document.Top - 20 * 1; // Ajustar según sea necesario
                    float radius = 15; // Radio del círculo
                    cb.EndText();

                    // Dibujar el círculo con color de fondo
                    cb.SetRGBColorFill(0x13, 0xA8, 0x9C); // Color de fondo del círculo
                    cb.Circle(centerX, centerY, radius);
                    cb.FillStroke();

                    // Posicionar el número dentro del círculo con color blanco
                    cb.BeginText();
                    cb.SetFontAndSize(BaseFont.CreateFont(), 15);
                    cb.SetRGBColorFill(255, 255, 255); // Color blanco para el número
                    cb.ShowTextAligned(Element.ALIGN_CENTER, categoryNumber.ToString(), centerX, centerY - 5, 0); // Ajustar la posición Y según sea necesario
                    cb.EndText();

                    // Añadir nombre de la nueva categoría
                    Paragraph categoryParagraph = new Paragraph(currentCategory, categoryFont);
                    categoryParagraph.IndentationLeft = 50; // Ajustar para alinear con el círculo
                    document.Add(categoryParagraph);
                }

                // Verificar si cambió la subcategoría
                if (item.FoodSubClassificationName != currentSubcategory)
                {
                    currentSubcategory = item.FoodSubClassificationName;

                    // Añadir nombre de la nueva subcategoría
                    Paragraph subcategoryParagraph = new Paragraph("  " + currentSubcategory, subcategoryFont);
                    subcategoryParagraph.IndentationLeft = 30; // Ajustar para alinear con la categoría
                    document.Add(subcategoryParagraph); // Indentado para subcategoría
                }

                // Tabla secundaria para el conjunto de imagen-nombre
                PdfPTable innerTable = new PdfPTable(1);
                innerTable.WidthPercentage = 100;
                innerTable.SpacingBefore = 30; // Espacio superior

                if (!string.IsNullOrEmpty(item.ImagePath))
                {
                    try
                    {
                        var imageUrl = new Uri(Configuration.FoodPath + item.ImagePath);
                        var webClient = new WebClient();
                        byte[] imageBytes = webClient.DownloadData(imageUrl);

                        using (var ms = new MemoryStream(imageBytes))
                        {
                            using (var originalImage = SKBitmap.Decode(ms))
                            {
                                int standardWidth = 100; // Ancho estándar
                                int standardHeight = 100; // Alto estándar

                                // Crear un nuevo bitmap con fondo blanco
                                using (var resizedImage = new SKBitmap(standardWidth, standardHeight))
                                {
                                    using (var canvas = new SKCanvas(resizedImage))
                                    {
                                        canvas.Clear(SKColors.White); // Fondo blanco
                                        var paint = new SKPaint
                                        {
                                            FilterQuality = SKFilterQuality.High
                                        };
                                        canvas.DrawBitmap(originalImage, new SKRect(0, 0, standardWidth, standardHeight), paint);
                                    }

                                    using (var image = SKImage.FromBitmap(resizedImage))
                                    {
                                        using (var resizedMs = new MemoryStream())
                                        {
                                            image.Encode(SKEncodedImageFormat.Png, 90).SaveTo(resizedMs);
                                            resizedMs.Position = 0;

                                            var pdfImage = iTextSharp.text.Image.GetInstance(resizedMs);
                                            pdfImage.ScaleToFit(standardWidth, standardHeight); // Asegurar que la imagen se ajuste al tamaño estándar
                                            PdfPCell imageCell = new PdfPCell(pdfImage);
                                            imageCell.Border = PdfPCell.NO_BORDER;
                                            imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                            innerTable.AddCell(imageCell);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al cargar la imagen desde la URL: {ex.Message}");
                    }
                }

                var foodNameParts = item.FoodName.Split(" - ");
                if (foodNameParts.Length > 1 && foodNameParts[1].Trim().Length > 0)
                {
                    // Añadir el nombre debajo de la imagen con el color deseado
                    string foodNameBeforeDash = foodNameParts[0].Trim(); // Usar Trim() para eliminar espacios en blanco adicionales
                    PdfPCell nameCell = new PdfPCell(new Phrase(foodNameBeforeDash, font)); // Usar el font con el color
                    nameCell.Border = PdfPCell.NO_BORDER; // Sin borde para la celda del nombre
                    nameCell.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el nombre horizontalmente
                    innerTable.AddCell(nameCell);

                    // Crear una nueva celda para el texto adicional
                    string additionalText = foodNameParts[1].Trim(); // Este es el texto adicional que quieres añadir
                    PdfPCell additionalTextCell = new PdfPCell(new Phrase(additionalText, font)); // Usar el mismo font
                    additionalTextCell.Border = PdfPCell.NO_BORDER; // Sin borde para la celda del texto adicional
                    additionalTextCell.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el texto adicional horizontalmente
                    innerTable.AddCell(additionalTextCell);
                }
                else
                {
                    string foodNameBeforeDash = item.FoodName.Trim(); // Usar Trim() para eliminar espacios en blanco adicionales
                    PdfPCell nameCell = new PdfPCell(new Phrase(foodNameBeforeDash, font)); // Usar el font con el color
                    nameCell.Border = PdfPCell.NO_BORDER; // Sin borde para la celda del nombre
                    nameCell.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el nombre horizontalmente
                    innerTable.AddCell(nameCell);
                }
                Console.WriteLine(item.FoodName);
                string typeProductName = item.TypeProduct;
                PdfPCell typeProductNameCell = new PdfPCell(new Phrase(typeProductName, font));
                typeProductNameCell.Border = PdfPCell.NO_BORDER;
                typeProductNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
                innerTable.AddCell(typeProductNameCell);

                string additionalText2 = "Tamaño: " + item.Quantity + " Onzas";
                PdfPCell additionalTextCell2 = new PdfPCell(new Phrase(additionalText2, font));
                additionalTextCell2.Border = PdfPCell.NO_BORDER;
                additionalTextCell2.HorizontalAlignment = Element.ALIGN_CENTER;
                innerTable.AddCell(additionalTextCell2);

                PdfPCell cell = new PdfPCell(innerTable);
                cell.Border = PdfPCell.NO_BORDER; // Sin borde para esta celda
                mainTable.AddCell(cell);

                count++;

                // Añadir la tabla principal al documento y comenzar una nueva página si es necesario
                if (count % 6 == 0 && count != foodList.Count)
                {
                    document.Add(mainTable);
                    document.NewPage();
                    mainTable = new PdfPTable(3);
                    mainTable.WidthPercentage = 100;
                }
            }

            // Añadir cualquier conjunto restante que no haya completado una página completa
            if (count % 6 != 0 || count == foodList.Count)
            {
                // Añadir celdas vacías si el último grupo no completa una fila
                int emptyCellsToAdd = 3 - (count % 3);
                if (count % 3 != 0)
                {
                    for (int i = 0; i < emptyCellsToAdd; i++)
                    {
                        PdfPCell emptyCell = new PdfPCell();
                        emptyCell.Border = PdfPCell.NO_BORDER;
                        mainTable.AddCell(emptyCell);
                    }
                }
                document.Add(mainTable);
            }

            document.Close();

            Console.WriteLine("Pdf generado correctamente en la ruta: " + filePath);
        }
    }


    static void GenerateIndexList()
    {
        foodService = new FoodService(Configuration);

        EResponseBase<FoodClassification> list = foodService.GetWithSubClassification();
        System.Collections.Generic.List<FoodClassification> listado = list.listado.ToList();

        var directoryPath = Path.Combine(Configuration.FilesPath);
        var filePath = Path.Combine(directoryPath, "Indicexd.pdf");

        if (FileExists(filePath))
        {
            System.IO.File.Delete(filePath);
        }

        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            BaseColor numberColor = new BaseColor(255, 255, 255);
            iTextSharp.text.Font numberFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 15, iTextSharp.text.Font.BOLD, numberColor);

            // Crear un Font con el color deseado
            BaseColor textColor = new BaseColor(0x13, 0xA8, 0x9C); // Color #13A89C
            iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 15, iTextSharp.text.Font.BOLD, textColor);

            // Crear un Font para el título
            iTextSharp.text.Font titleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18, iTextSharp.text.Font.BOLD, textColor);

            // Crear y añadir el título centrado
            Paragraph title = new Paragraph("LISTA DE ALIMENTOS AUTORIZADOS", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);

            // Añadir un espacio después del título
            document.Add(new Paragraph("\n"));

            int indexNumber = 1;

            string category = "";
            string subCategory = "";

            foreach (var item in listado)
            {
                if (category != item.FoodClassificationName)
                {
                    // Crear una tabla con dos columnas
                    PdfPTable table = new PdfPTable(2);
                    table.WidthPercentage = 100;
                    table.SetWidths(new float[] { 1, 9 });

                    // Crear una celda para el círculo con el número
                    PdfPCell circleCell = new PdfPCell();
                    circleCell.Border = PdfPCell.NO_BORDER;
                    circleCell.Padding = 5;

                    // Crear un template para dibujar el círculo
                    PdfTemplate template = writer.DirectContent.CreateTemplate(30, 30);
                    float radius = 10f; // Radio del círculo
                    float circleX = 15f; // Posición X del círculo
                    float circleY = 15f; // Posición Y del círculo

                    // Dibujar el círculo en el template
                    template.SetColorFill(textColor);
                    template.Circle(circleX, circleY, radius);
                    template.Fill();

                    // Añadir el número dentro del círculo
                    ColumnText.ShowTextAligned(template, Element.ALIGN_CENTER, new Phrase(indexNumber.ToString(), numberFont), circleX, circleY - 5, 0);

                    // Convertir el template en una imagen
                    iTextSharp.text.Image circleImage = iTextSharp.text.Image.GetInstance(template);
                    circleCell.AddElement(circleImage);
                    table.AddCell(circleCell);

                    // Añadir el nombre de la categoría
                    PdfPCell textCell = new PdfPCell(new Phrase($"{item.FoodClassificationName}", titleFont));
                    textCell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    textCell.Border = PdfPCell.NO_BORDER;
                    table.AddCell(textCell);

                    document.Add(table);

                    category = item.FoodClassificationName;
                    indexNumber++;
                }

                System.Collections.Generic.List<FoodSubClassification> subClassifications = item.FoodSubClassifications.ToList();

                foreach (var subItem in subClassifications)
                {
                    if (subCategory != subItem.FoodSubClassificationName)
                    {
                        // Añadir el nombre de la subcategoría
                        Paragraph subParagraph = new Paragraph($"{subItem.FoodSubClassificationName}", font);
                        subParagraph.IndentationLeft = 50f; // Ajustar la indentación para que el texto no se superponga con el círculo
                        document.Add(subParagraph);

                        subCategory = subItem.FoodSubClassificationName;
                    }
                }
            }

            document.Close();
        }
    }

    static void GeneratePDF()
    {
        var directoryPath = Path.Combine(Configuration.FilesPath);
        var fileNames = new string[]
        {
        "Portada.pdf",
        "Indicexd.pdf",
        "Alimentosxd.pdf"
        };

       
        var allFilesExist = true;

        foreach (var fileName in fileNames)
        {
            var filePath = Path.Combine(directoryPath, fileName);

            if (!FileExists(filePath))
            {
                allFilesExist = false;
                break;
            }
        }

        if (allFilesExist)
        {
            var outputFilePath = Path.Combine(directoryPath, "AlimentosAprobadosWIC2024.pdf");

            if (FileExists(outputFilePath))
            {
                System.IO.File.Delete(outputFilePath);
            }

            var firstFilePath = Path.Combine(directoryPath, fileNames.First());
            using (var pdfStamper = new PdfStamper(new PdfReader(firstFilePath), new FileStream(outputFilePath, FileMode.Append)))
            {
                var form = pdfStamper.AcroFields;

                // Cambia el nombre del campo según tu PDF
                var fieldName = "fecha";

                DateTime fecha = DateTime.Now;

                // Establecer el valor del campo de fecha
                form.SetField(fieldName, fecha.ToString("MMMM yyyy"));

                pdfStamper.AcroFields.SetFieldProperty(fieldName, "setfflags", PdfFormField.FF_READ_ONLY, null);

                int pageIndex = 2;

                for (int i = 1; i < fileNames.Length; i++)
                {
                    var filePath = Path.Combine(directoryPath, fileNames[i]);
                    var pdfReader = new PdfReader(filePath);

                    // Inserta todas las páginas del archivo PDF
                    for (int j = 1; j <= pdfReader.NumberOfPages; j++)
                    {
                        var pageSize = pdfReader.GetPageSizeWithRotation(j);
                        pdfStamper.InsertPage(pageIndex, pageSize);
                        var pdfContentByte = pdfStamper.GetUnderContent(pageIndex);
                        var importedPage = pdfStamper.GetImportedPage(pdfReader, j);

                        // Ajustar la orientación de la página
                        if (pageSize.Width > pageSize.Height)
                        {
                            pdfContentByte.AddTemplate(importedPage, 0, -1, 1, 0, 0, pageSize.Height);
                        }
                        else
                        {
                            pdfContentByte.AddTemplate(importedPage, 1, 0, 0, 1, 0, 0);
                        }

                        pageIndex++;
                    }
                }
            }

            // Descargar el archivo combinado
            var fileBytes = System.IO.File.ReadAllBytes(outputFilePath);
            //return File(fileBytes, "application/pdf", "AlimentosAprobadosWIC2023.pdf");
        }
        else
        {
            //return StatusCode(500, "Error al procesar los archivos ");
        }
    }

    static bool FileExists(string filePath)
    {
        try
        {
            var fileInfo = new FileInfo(filePath);
            return fileInfo.Exists;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
