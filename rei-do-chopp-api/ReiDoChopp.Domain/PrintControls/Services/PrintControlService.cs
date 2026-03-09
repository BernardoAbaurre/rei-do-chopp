using System;
using System.Text;
using System.Threading.Tasks;
using ReiDoChopp.Domain.PrintControls.Entities;
using ReiDoChopp.Domain.PrintControls.Enums;
using ReiDoChopp.Domain.PrintControls.Repositories;
using ReiDoChopp.Domain.PrintControls.Services.Interfaces;
using ReiDoChopp.Domain.Users.Entities;
using ReiDoChopp.Domain.Utils.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ReiDoChopp.Domain.PrintControls.Services
{
    public class PrintControlsService : IPrintControlsService
    {
        private readonly IPrintControlsRepository printControlsRepository;

        public PrintControlsService(IPrintControlsRepository printControlsRepository)
        {
            this.printControlsRepository = printControlsRepository;
        }

        public async Task<PrintControl> ChangeStatusAsync(int id, PrintControlStatusEnum status)
        {
            PrintControl printControl = await ValidateAsync(id);

            printControl.SetStatus(status);

            printControlsRepository.Edit(printControl);

            return printControl;
        }

        public async Task<PrintControl> InsertAsync(User user, string content)
        {
            StringBuilder text = new();
            text.Append("[BIG_LINE][ALIGN_CENTER][BIG_FONT]");
            text.Append("Rei do Chopp\n[TEXT_NORMAL][BIG_LINE]");
            text.Append("Av. Agua M.nha - 551\nSanta Monica, Guarapari - ES\nCEP: 29221-000\nTEL: 27 99617-4836\n");
            text.Append("[LINE]");
            text.Append(content);

            PrintControl printControl = new PrintControl(user, text.ToString());

            await printControlsRepository.InsertAsync(printControl);
            
            return printControl;
        }

        public async Task<PrintControl> ValidateAsync(int id)
        {
            PrintControl printControl = await printControlsRepository.FindByIdAsync(id);

            if (printControl == null)
            {
                throw new RegisterNotFound(id);
            }

            return printControl;
        }
    }
}
