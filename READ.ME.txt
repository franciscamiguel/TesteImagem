Converter de imagem para base64:
https://www.base64-image.de/

No Postman:

Colocar o localhost da Web API, exemplo: https://localhost:44310/Image
Definir como POST

Definir como "raw" (texto cru) e depois selecionar JSON

{
  "title": "teste",
  "photo": "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEBLAEsAAD..."
}
// passar imagem em base64 extraída no primeiro link de conversão online

Enviar a solicitação via Postman:

Copiar o texto de resposta dentro do atributo "photo"
Abrir o site:
https://base64.guru/converter/decode/image

Converter de base64 para imagem