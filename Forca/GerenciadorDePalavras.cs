using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forca
{
    public class GerenciadorDePalavras
    {
        private ManipuladorXML _manipuladorXML;
        private Random _geradorDeNumeros;

        public GerenciadorDePalavras()
        {
            _manipuladorXML = new ManipuladorXML();
            _geradorDeNumeros = new Random();
        }

        public string SelecionePalavra(string categoria) {
            return _manipuladorXML.ObtenhaPalavra(categoria, ObtenhaNumero(categoria));
        }

        public IList<string> ObtenhaCategorias()
        {
            return _manipuladorXML.ObtenhaCategorias();
        }

        private int ObtenhaNumero(string categoria)
        {
            return _geradorDeNumeros.Next(1, _manipuladorXML.QuantidadeDeElementos(categoria));
        }
    }
}
