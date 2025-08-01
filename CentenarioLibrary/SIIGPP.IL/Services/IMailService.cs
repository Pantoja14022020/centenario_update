using System.Threading.Tasks;
using SIIGPP.IL.Models.Agenda;
public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}