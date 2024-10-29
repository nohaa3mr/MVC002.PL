using MVC002.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace MVC002.PL.Helpers
{
	public static class EmailSettings
	{
		public static  void SendByEmail(EmailAddress email)
		{
			var Client = new SmtpClient("smtp.gmail.com", 465); //As a performence not likely recommended.
			Client.EnableSsl = true;   //Encrypted link
			Client.Credentials = new NetworkCredential("kookygamal1@gmail.com" , "ctkv uojc afgq silb");
		Client.Send("kookygamal1@gmail.com", email.To, email.Subject, email.Body);
		}

	}
}
