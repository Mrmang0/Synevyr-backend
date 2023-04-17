using System.Xml.Serialization;

namespace Synevyr.Models;

// ICY VEINS

[XmlRoot(ElementName="guid")]
public class RSSGuid { 

    [XmlAttribute(AttributeName="isPermaLink")] 
    public bool IsPermaLink { get; set; } 

    [XmlText] 
    public string Text { get; set; } 
}

[XmlRoot(ElementName="item")]
public class Item { 

    [XmlElement(ElementName="title")] 
    public string Title { get; set; } 

    [XmlElement(ElementName="link")] 
    public string Link { get; set; } 

    [XmlElement(ElementName="description")] 
    public string Description { get; set; } 

    [XmlElement(ElementName="guid")] 
    public RSSGuid Guid { get; set; } 

    [XmlElement(ElementName="pubDate")] 
    public string PubDate { get; set; } 
}

[XmlRoot(ElementName="channel")]
public class Channel { 

    [XmlElement(ElementName="title")] 
    public string Title { get; set; } 

    [XmlElement(ElementName="link")] 
    public string Link { get; set; } 

    [XmlElement(ElementName="description")] 
    public string Description { get; set; } 

    [XmlElement(ElementName="language")] 
    public string Language { get; set; } 

    [XmlElement(ElementName="item")] 
    public List<Item> Item { get; set; } 
}

[XmlRoot(ElementName="rss")]
public class Rss { 

    [XmlElement(ElementName="channel")] 
    public Channel Channel { get; set; } 

    [XmlAttribute(AttributeName="version")] 
    public double Version { get; set; } 

    [XmlText] 
    public string Text { get; set; } 
}

// WOW HEAD


[XmlRoot(ElementName="image")]
public class Image { 

	[XmlElement(ElementName="url")] 
	public string Url { get; set; } 

	[XmlElement(ElementName="title")] 
	public string Title { get; set; } 

	[XmlElement(ElementName="link")] 
	public string Link { get; set; } 
}


[XmlRoot(ElementName="content")]
public class Content { 

	[XmlAttribute(AttributeName="media")] 
	public string Media { get; set; } 

	[XmlAttribute(AttributeName="url")] 
	public string Url { get; set; } 

	[XmlAttribute(AttributeName="medium")] 
	public string Medium { get; set; } 

	[XmlAttribute(AttributeName="type")] 
	public string Type { get; set; } 

	[XmlAttribute(AttributeName="height")] 
	public int Height { get; set; } 

	[XmlAttribute(AttributeName="width")] 
	public int Width { get; set; } 
}

[XmlRoot(ElementName="item")]
public class WowHeadItem { 

	[XmlElement(ElementName="title")] 
	public string Title { get; set; } 

	[XmlElement(ElementName="link")] 
	public string Link { get; set; } 

	[XmlElement(ElementName="description")] 
	public string Description { get; set; } 

	[XmlElement(ElementName="category")] 
	public string Category { get; set; } 

	[XmlElement(ElementName="pubDate")] 
	public string PubDate { get; set; } 

	[XmlElement(ElementName="guid")] 
	public WowHeadGuid Guid { get; set; } 

	[XmlElement(ElementName="content")] 
	public Content Content { get; set; }

}

[XmlRoot(ElementName="channel")]
public class WowHeadChannel { 

	[XmlElement(ElementName="title")] 
	public string Title { get; set; } 

	[XmlElement(ElementName="link")] 
	public string Link { get; set; } 

	[XmlElement(ElementName="description")] 
	public string Description { get; set; } 

	[XmlElement(ElementName="image")] 
	public Image Image { get; set; } 

	[XmlElement(ElementName="language")] 
	public string Language { get; set; } 

	[XmlElement(ElementName="ttl")] 
	public int Ttl { get; set; } 

	[XmlElement(ElementName="lastBuildDate")] 
	public string LastBuildDate { get; set; } 

	[XmlElement(ElementName="item")] 
	public List<WowHeadItem> Item { get; set; } 
}

[XmlRoot(ElementName="rss")]
public class WowheadRss { 

	[XmlElement(ElementName="channel")] 
	public WowHeadChannel Channel { get; set; } 

	[XmlAttribute(AttributeName="version")] 
	public double Version { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="guid")]
public class WowHeadGuid { 

	[XmlAttribute(AttributeName="isPermalink")] 
	public bool IsPermalink { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}