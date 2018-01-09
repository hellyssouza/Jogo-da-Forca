using System;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

public class ManipuladorXML
{
    private XDocument _documento;

    public ManipuladorXML()
    {
        _documento = XDocument.Load("../../../Palavras.xml");
    }

    public string ObtenhaPalavra(string categoria, int codigo)
    {
        return SelecioneElementos(categoria)
                         .Where(y => y.Attribute("codigo").Value == codigo.ToString())
                         .FirstOrDefault().Value;
    }

    public IList<string> ObtenhaCategorias()
    {
        return _documento.Descendants("Lista").Select(x => x.Attribute("categoria").Value).ToList();
    }

    public int QuantidadeDeElementos(string categoria)
    {
        return SelecioneElementos(categoria).Count;
    }

    private IList<XElement> SelecioneElementos(string categoria)
    {
        return _documento.Descendants("Lista")
                         .Where(x => x.Attribute("categoria").Value == categoria)
                         .FirstOrDefault().Descendants().ToList();
    }
}
