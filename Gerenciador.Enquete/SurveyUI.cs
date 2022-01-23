namespace Gerenciador.Enquete
{
    class SurveyUI
    {
        private Survey survey;

        private string surveyFile;

        public void Start()
        {
            while (true)
            {
                string option = ShowMainMenu();

                if (option == "1")
                {
                    ShowCreateMenu();
                    ShowSurveyMenu();
                }
                else if (option == "2")
                {
                    ShowLoadMenu();
                    ShowSurveyMenu();
                }
                else if (option == "3")
                {
                    return;
                }
            }
        }

        private string ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("MENU PRINCIPAL");
                Console.WriteLine("--------------\n");

                Console.WriteLine("1 - Criar uma enquete");
                Console.WriteLine("2 - Carregar uma enquete");
                Console.WriteLine("3 - Sair");

                Console.Write("O que você precisa fazer? => ");

                string option = Console.ReadLine();

                if (option != "1" && option != "2" && option != "3")
                {
                    continue;
                }

                return option;
            }
        }

        private void ShowCreateMenu()
        {
            survey = new Survey();

            surveyFile = null;

            Console.Clear();

            Console.WriteLine("CRIAR UMA NOVA ENQUETE");
            Console.WriteLine("----------------------\n");

            while (true)
            {
                Console.Write("Pergunta: ");

                string question = Console.ReadLine();

                if (!string.IsNullOrEmpty(question))
                {
                    survey.Question = question;
                    break;
                }
            }

            int numOptions;

            while (true)
            {
                Console.Write("Quantas opções a pergunta vai ter? ");

                try
                {
                    numOptions = int.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {

                }
            }

            for (int i = 0; i < numOptions; i++)
            {
                string id;
                string text;

                while (true)
                {
                    Console.Write("ID da opção {0}: ", i + 1);

                    id = Console.ReadLine();

                    if (!string.IsNullOrEmpty(id))
                    {
                        break;
                    }
                }

                while (true)
                {
                    Console.Write("Texto da opção {0}: ", i + 1);

                    text = Console.ReadLine();

                    if (!string.IsNullOrEmpty(text))
                    {
                        break;
                    }
                }

                survey.SetOption(id, text);
            }

            Console.WriteLine("Opções adicionadas com sucesso! Veja a enquete:\n");
            Console.WriteLine(survey.GetFormattedSurvey());

            while (true)
            {
                Console.Write("Digite o caminho do arquivo para salvar a enquete: ");

                string filePath = Console.ReadLine();

                if (!string.IsNullOrEmpty(filePath))
                {
                    try
                    {
                        SurveyIO.SaveToFile(survey, filePath);
                        surveyFile = filePath;
                        break;
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Ocorreu um erro ao salvar o arquivo: {0}", e.Message);
                    }
                }
            }

            Console.WriteLine("Enquete salva em \"{0}\". Pressione ENTER para continuar...", surveyFile);

            Console.ReadLine();
        }

        private void ShowLoadMenu()
        {
            survey = new Survey();

            Console.Clear();

            Console.WriteLine("CARREGAR ENQUETE");
            Console.WriteLine("----------------\n");

            while (true)
            {
                Console.Write("Digite o nome do arquivo da enquete: ");

                string filePath = Console.ReadLine();

                if (!string.IsNullOrEmpty(filePath))
                {
                    try
                    {
                        SurveyIO.LoadFromFile(survey, filePath);
                        surveyFile = filePath;

                        Console.WriteLine("A enquete foi carregada com sucesso!. Pressione ENTER para continuar...");
                        Console.ReadLine();

                        break;
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Ocorreu um erro ao abrir o arquivo: {0}", e.Message);
                    }
                }
            }
        }

        private void ShowSurveyMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("MENU DE ENQUETE");
                Console.WriteLine("---------------\n");
                Console.WriteLine("Enquete ativa: \"{0}\"", survey.Question);
                Console.WriteLine("Número de votos: {0}\n", survey.VoteCount);

                Console.WriteLine("1 - Votar na enquete");
                Console.WriteLine("2 - Ver resultados da enquete");
                Console.WriteLine("3 - Voltar ao menu principal");

                Console.Write("Escolha uma opção => ");

                string option = Console.ReadLine();

                if (option == "1")
                {
                    ShowVoteMenu();
                }
                else if (option == "2")
                {
                    ShowSurveyResults();
                }
                else if (option == "3")
                {
                    break;
                }
            }
        }

        private void ShowVoteMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("VOTAR");
                Console.WriteLine("-----\n");

                Console.WriteLine("Quantidade de votos: {0}\n", survey.VoteCount);

                Console.WriteLine(survey.GetFormattedSurvey());

                Console.Write("Escolha uma opção => ");

                Option optio;
                string vote;

                bool valid = survey.Vote(out optio, out vote);

                if (valid)
                {
                    Console.Write("Obrigado pelo seu voto! Deseja continuar votando? (S/N): ");

                    string yn = Console.ReadLine();

                    if (yn != "S" && yn != "s")
                    {
                        break;
                    }
                }
            }

            SurveyIO.SaveToFile(survey, surveyFile);

            Console.Write("Fim da votação. Pressione ENTER para continuar...");

            Console.ReadLine();
        }

        private void ShowSurveyResults()
        {
            Console.Clear();

            Console.WriteLine("RESULTADO DA ENQUETE");
            Console.WriteLine("--------------------\n");

            List<OptionScore> scores = survey.CalculateScores();

            Console.WriteLine("{0,-23} | {1,-5}", "Opção", "Votos");
            Console.WriteLine("------------------------------- ");

            foreach (OptionScore score in scores)
            {
                Console.WriteLine("{0,-3}{1,-20} | {2,5}", score.Option.Id, score.Option.Text, score.Count);
            }

            Console.Write("\nPressione ENTER para continuar...");

            Console.ReadLine();
        }
    }
}
