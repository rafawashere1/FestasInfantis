﻿using FestasInfantis.Dominio.ModuloTema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FestasInfantis.Infra.Dados.Arquivo.Compartilhado
{
    public class ContextoDados
    {
        private const string NOME_ARQUIVO = "Compartilhado\\FestasInfantis.json";

        public List<Tema> temas;

        public ContextoDados()
        {
            temas = new List<Tema>();
        }

        public ContextoDados(bool carregarDados) : this()
        {
            if (carregarDados)
                CarregarRegistrosParaArquivoJson();
        }

        public void EnviarRegistrosParaArquivoJson()
        {
            File.WriteAllText(NOME_ARQUIVO, JsonSerializer.Serialize(this, ObterConfiguracaoJson()));
        }

        public void CarregarRegistrosParaArquivoJson()
        {

            if (File.Exists(NOME_ARQUIVO))
            {
                if (File.ReadAllText(NOME_ARQUIVO).Length > 0)
                {
                    ContextoDados ctx = JsonSerializer.Deserialize<ContextoDados>(File.ReadAllText(NOME_ARQUIVO), ObterConfiguracaoJson());

                    temas = ctx.temas;
                }
            }
        }

        private static JsonSerializerOptions ObterConfiguracaoJson()
        {
            JsonSerializerOptions options = new()
            {
                IncludeFields = true,
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };

            return options;
        }
    }
}