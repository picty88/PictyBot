﻿using Microsoft.Bot.Builder.Dialogs;
using System; 
using System.Threading.Tasks;
using SimpleEchoBot.Services;
using Microsoft.Bot.Connector;
using SimpleEchoBot.Model;
using System.Diagnostics;
using SimpleEchoBot.Extension;
using System.Globalization;
using System.Collections.Generic;

namespace SimpleEchoBot.Dialogs
{
    [Serializable]
    public class RRHHPeopleDialog : IDialog<object>
    {
        private UserLogin user;
        private People people;
        private ResultAutenticate login;
        public RRHHPeopleDialog(UserLogin _user, People _people)
        {
            user = _user;
            people = _people;
        }
      
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Para obtener información de Recursos Humanos necesitamos sus credenciales.");

            PromptDialog.Text(context, ResumePeopleUserName, "Por favor, ingrese el usuario con el cual inicia sesión en su computador:");
             
        }
        private async Task ResumePeopleUserName(IDialogContext context, IAwaitable<string> result)
        {
            var answer = await result;
            user.UserOrEmailAdrees = answer;
            PromptDialog.Text(context, GetInformationRRHH, "Ingrese su Password:");
        }
       
        private async Task GetInformationRRHH(IDialogContext context, IAwaitable<string> result)
        {
            var answer = await result;
            user.Password = answer;
            try
            {
                PeopeAppService searchService = new PeopeAppService();
                login = await searchService.Autenticate(user);
                login.Nombres = "Juan Perez Perez";
                if (login.Result)
                {
                    var message = context.MakeMessage();
                    message.Attachments = new List<Attachment>();
                    message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                    message.Attachments.Add(SettingsCardDialog.CardRRHH().ToAttachment());
                    message.Text = $"Hola, "+ login.Nombres+" estas en la sección Recursos Humanos";
                    await context.PostAsync(message);
                    context.Wait(MessageRecievedAsync);
                }
                else
                {

                    await context.PostAsync(string.Format(CultureInfo.CurrentCulture, "Los datos ingresados no son correctos. Por favor intente de nuevo", user.UserOrEmailAdrees));
                    PromptDialog.Text(context, ResumePeopleUserName, "Por favor, ingrese el usuario con el cual inicia sesión en su computador:");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error when searching for people: {e.Message}");
            }
         
        }

        public virtual async Task MessageRecievedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var CategoryName = await result;
            try
            {
                PeopeAppService searchService = new PeopeAppService();
                Holiday holiday = await searchService.GetRRHHHolidays(login);
                holiday.Nombres = login.Nombres;
                if (CategoryName != null)
                {
                    switch (CategoryName.Text)
                    {
                        case SettingsCardDialog.RRHHHolidays:
                            CardUtil.ShowRRHHHolidaysCard(CategoryName, holiday);
                            break;
                        case SettingsCardDialog.RRHHvoucher:
                            CardUtil.ShowRRHHvoucherCard(CategoryName, holiday);
                            break;
                  
                        default:
                            await context.PostAsync(string.Format(CultureInfo.CurrentCulture, "La opción {0} no es válida. Por favor intente de nuevo", CategoryName));
                            break;

                    }
                }
                else
                {

                    await context.PostAsync(string.Format(CultureInfo.CurrentCulture, "La opción {0} no es válida. Por favor intente de nuevo", CategoryName));
                    context.Wait(this.MessageRecievedAsync);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error when searching for people: {e.Message}");
            }
            context.Done<object>(null);
        }
    }
}