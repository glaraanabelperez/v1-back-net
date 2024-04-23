//using Microsoft.AspNetCore.Mvc;

//namespace api.abrazos.Controllers
//{
//    public class ImageController
//    {
//        string ruta = "D:\\Inetpub\\vhosts\\panaleracolores.com.ar\\httpdocs\\assets";
//        string rutaLocal =
//        /// <summary>
//        /// Borra la imagen
//        /// </summary>
//        /// <param name="data">Datos de la imagen</param>
//        /// <returns><c>1</c> Si se guardaron los datos</returns>
//        [Route("api/images/delete")]
//        [HttpPost]
//        public async Task<IActionResult> DeleteImage(string imageName)
//        {
//            try
//            {

//               string pathString = System.IO.Path.Combine(this.ruta, image.ProductId.ToString());

//               if (image.ProductImageId != 0 && image.ProductImageId.HasValue && !String.IsNullOrEmpty(image.Name))
//               {

//                   int result = imagenLogic.Delete(image);
//                   //if (File.Exists(pathString + "\\" + image.Name) && result > 0)
//                   //{
//                   System.IO.File.Delete(System.IO.Path.Combine(pathString, image.Name));
//                   //}
//               }
//               else
//               {
//                   return BadRequest("El modelo de datos esta incorrecto o vacio");
//               }
//            }
//            catch (Exception ex)
//            {
//                return Content(HttpStatusCode.InternalServerError, ex.Message);
//            }
//            return Ok();
//        }

//        [Route("api/images")]
//        [HttpPost]
//        public async Task<IActionResult> verifyImageOnserver([FromBody] ProductImageDto image)
//        {

//            if (image.ProductId != 0)
//            {
//                string pathString = System.IO.Path.Combine(this.ruta, image.ProductId.ToString());

//                if (!File.Exists((pathString + "\\" + image.Name)))
//                {
//                    return Ok();
//                }
//                else
//                {
//                    return BadRequest();
//                }

//            }

//            return InternalServerError();
//        }

//        [Route("api/insert_Image/{productId}")]
//        [HttpPut]
//        public async Task<IActionResult> InsertImage(int productId)
//        {
//            string pathString = System.IO.Path.Combine(this.ruta, productId.ToString());
//            if (productId != 0)
//            {
//                if (!System.IO.File.Exists(pathString))
//                {
//                    System.IO.Directory.CreateDirectory(pathString);
//                }
//                try
//                {
//                    var httpRequest = HttpContext.Current.Request;
//                    if (httpRequest.Files.Count > 0)
//                    {
//                        for (var i = 0; i < httpRequest.Files.Count; i++)
//                        {
//                            var postedFile = httpRequest.Files[i];
//                            var filePath = System.IO.Path.Combine(pathString, postedFile.FileName);
//                            postedFile.SaveAs(filePath);
//                            imagenLogic.Save(postedFile.FileName, productId);

//                            byte[] datosArchivo = null;
//                            using (var binaryReader = new BinaryReader(postedFile.InputStream))
//                            {
//                                datosArchivo = binaryReader.ReadBytes(postedFile.ContentLength);
//                            }

//                            System.IO.File.WriteAllBytes(filePath, datosArchivo);

//                        }
//                        return Ok(pathString);
//                    }
//                    else
//                    {
//                        return BadRequest(pathString);
//                    }
//                }
//                catch (Exception e)
//                {
//                    return BadRequest(e.Message);
//                }
//            }
//            else
//            {
//                return BadRequest();
//            }

//        }
//    }
//}
