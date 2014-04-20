using System;
using System.Xml;
using System.Collections;

namespace Whf.TuoPu.Common
{
	/// <summary>
	/// 作用：提供对于全局配置信息的管理，用来加载和缓存PalauGlobalConfig.xml配置信息
	/// 日期：2003/10/9
	/// </summary>
	public class ConfigManager
	{
		/// <summary>
		/// 默认构造函数
		/// </summary>
		public ConfigManager()
		{
			
		}

		/// <summary>
		/// 在初始化的时候加载配置信息到hashtable
		/// </summary>
		/// <param name="ConfigFile">配置文件的绝对定位路径</param>
		public ConfigManager(string ConfigFile)
		{
			this.LoadConfig(ConfigFile);
		}

		/// <summary>
		/// 定义内部存储使用的Hashtable，用来缓存配置信息
		/// </summary>
		private Hashtable mHst=new Hashtable();

		/// <summary>
		/// 作用：索引器，提供需要的时候就开始逐次缓存配置信息
		/// </summary>
		public object this[string ConfigKey]
		{
			get
			{
				return this.mHst[ConfigKey];
			}
			
		}

		/// <summary>
		/// 监测在配置管理器中是否存在某一个Key
		/// </summary>
		/// <param name="KeyName">键名称</param>
		/// <returns>true - 存在  false - 不存在</returns>
		public bool HasKey(string KeyName)
		{
			try
			{
				return this.mHst.ContainsKey(KeyName);
			}
			catch(Exception ex)  //忽略掉系统错误
			{
				return false;
			}
		}
		/// <summary>
		/// 作用：加载配置信息到缓冲器
		/// </summary>
		/// <param name="ConfigFile">配置文件的绝对路径，比如：d:\xxx\PalauGlobalConfig.xml</param>
		/// <returns>true - 加载成功  false - 加载失败 </returns>
		public bool LoadConfig(string ConfigFile)
		{
			try
			{
				string strValue="";

				string strName="";

                XmlDocument objDom = new XmlDocument();
				objDom.Load(ConfigFile);

				XmlNodeList objNodeList=objDom.SelectSingleNode("PalauGlobalConfig").ChildNodes;

				//循环遍历所有需要加载的字节点信息，缓存到Hashtable中
				foreach(XmlNode objNode in objNodeList)
				{

					//如果此节点是元素的话（过滤掉XML文件中的注释信息）
					if(objNode.NodeType==XmlNodeType.Element)
					{
						strValue=objNode.InnerText.ToString().Trim();
						strName=objNode.Name.ToString().Trim();

						mHst[strName]=strValue;
					}
				}

				return true;

			}
			catch // 这里可以直接catch
			{
				return false;
			}
		}
	}
}
