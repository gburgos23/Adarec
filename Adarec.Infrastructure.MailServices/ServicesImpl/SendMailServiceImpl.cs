using Adarec.Application.DTO.DTOs;
using Adarec.Infrastructure.MailServices.Models;
using Adarec.Infrastructure.MailServices.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Adarec.Infrastructure.MailServices.ServicesImpl
{
    public class SendMailServiceImpl : ISendMailService
    {
        private static async Task SendMailAsync(string to, string subject, string body, List<MailAttachment>? adjuntos = null)
        {
            try
            {
                var smtpHost = Environment.GetEnvironmentVariable("smtpHost");
                var smtpPort = int.TryParse(Environment.GetEnvironmentVariable("smtpPort"), out var port) ? port : 587;
                var smtpUser = Environment.GetEnvironmentVariable("smtpUser");
                var smtpPass = Environment.GetEnvironmentVariable("smtpPass");
                var smtpFrom = Environment.GetEnvironmentVariable("smtpFrom");

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Adarec Solutions", smtpFrom));
                message.To.Add(MailboxAddress.Parse(to));
                message.Subject = subject;

                var builder = new BodyBuilder
                {
                    HtmlBody = body
                };

                if (adjuntos != null)
                {
                    foreach (var adj in adjuntos)
                    {
                        builder.Attachments.Add(adj.FileName, adj.Content, ContentType.Parse(adj.MimeType));
                    }
                }

                message.Body = builder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(smtpUser, smtpPass);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al enviar el correo: {ex.Message}", ex);
            }
        }

        public async Task SendOrderCompletionMailAsync(OrderDto order)
        {
            string subject = $"Su orden {order.OrderId} ha sido completada";
            var bodyBuilder = new System.Text.StringBuilder();

            bodyBuilder.AppendLine($"Estimado/a {order.Customer.Name},<br>");
            bodyBuilder.AppendLine($"<br>Su orden #{order.OrderId} ha sido completada.<br>");
            bodyBuilder.AppendLine("A continuación el detalle de los equipos:<br><br>");
            bodyBuilder.AppendLine("<table border='1' cellpadding='5' cellspacing='0'>");
            bodyBuilder.AppendLine("<tr><th>Equipo</th><th>Cantidad</th><th>Especificaciones</th><th>Estado</th><th>Imagen</th></tr>");

            var attachments = new List<MailAttachment>();
            int i = 1;
            foreach (var equipo in order.Devices)
            {
                string equipoNombre = $"{equipo.BrandName} {equipo.ModelName}";
                string estado = equipo.ItemStatus ?? "Desconocido";
                string specs = equipo.DeviceSpecs ?? "";
                string imgTag = "";

                if (!string.IsNullOrEmpty(equipo.SolutionPhoto) && File.Exists(equipo.SolutionPhoto))
                {
                    try
                    {
                        byte[] bytes = await File.ReadAllBytesAsync(equipo.SolutionPhoto);
                        string contentId = $"img{i}@adarec";
                        attachments.Add(new MailAttachment
                        {
                            FileName = $"Solucion_{i}.jpg",
                            Content = bytes,
                            MimeType = "image/jpeg"
                        });
                        imgTag = $"<img src='cid:{contentId}' width='120' />";
                    }
                    catch
                    {
                        imgTag = "<span style='color:red'>Imagen no disponible</span>";
                    }
                }
                else
                {
                    imgTag = "<span style='color:gray'>Sin imagen</span>";
                }

                bodyBuilder.AppendLine($"<tr><td>{equipoNombre}</td><td>{equipo.Quantity}</td><td>{specs}</td><td>{estado}</td><td>{imgTag}</td></tr>");
                i++;
            }
            bodyBuilder.AppendLine("</table><br>");

            if (order.LastComment != null && !string.IsNullOrWhiteSpace(order.LastComment.Comment))
            {
                bodyBuilder.AppendLine($"<b>El comentario de cierre es:</b> {order.LastComment.Comment}<br>");
            }
            else
            {
                bodyBuilder.AppendLine("<b>No hay comentario de cierre.</b><br>");
            }

            bodyBuilder.AppendLine("Gracias por confiar en nosotros.");

            await SendMailAsync(order.Customer.Email, subject, bodyBuilder.ToString(), attachments);
        }
    }
}