using System;
using System.Xml;
using System.Collections;

namespace Whf.TuoPu.Common
{
	/// <summary>
	/// ���ã��ṩ����ȫ��������Ϣ�Ĺ����������غͻ���PalauGlobalConfig.xml������Ϣ
	/// ���ڣ�2003/10/9
	/// </summary>
	public class ConfigManager
	{
		/// <summary>
		/// Ĭ�Ϲ��캯��
		/// </summary>
		public ConfigManager()
		{
			
		}

		/// <summary>
		/// �ڳ�ʼ����ʱ�����������Ϣ��hashtable
		/// </summary>
		/// <param name="ConfigFile">�����ļ��ľ��Զ�λ·��</param>
		public ConfigManager(string ConfigFile)
		{
			this.LoadConfig(ConfigFile);
		}

		/// <summary>
		/// �����ڲ��洢ʹ�õ�Hashtable����������������Ϣ
		/// </summary>
		private Hashtable mHst=new Hashtable();

		/// <summary>
		/// ���ã����������ṩ��Ҫ��ʱ��Ϳ�ʼ��λ���������Ϣ
		/// </summary>
		public object this[string ConfigKey]
		{
			get
			{
				return this.mHst[ConfigKey];
			}
			
		}

		/// <summary>
		/// ��������ù��������Ƿ����ĳһ��Key
		/// </summary>
		/// <param name="KeyName">������</param>
		/// <returns>true - ����  false - ������</returns>
		public bool HasKey(string KeyName)
		{
			try
			{
				return this.mHst.ContainsKey(KeyName);
			}
			catch(Exception ex)  //���Ե�ϵͳ����
			{
				return false;
			}
		}
		/// <summary>
		/// ���ã�����������Ϣ��������
		/// </summary>
		/// <param name="ConfigFile">�����ļ��ľ���·�������磺d:\xxx\PalauGlobalConfig.xml</param>
		/// <returns>true - ���سɹ�  false - ����ʧ�� </returns>
		public bool LoadConfig(string ConfigFile)
		{
			try
			{
				string strValue="";

				string strName="";

                XmlDocument objDom = new XmlDocument();
				objDom.Load(ConfigFile);

				XmlNodeList objNodeList=objDom.SelectSingleNode("PalauGlobalConfig").ChildNodes;

				//ѭ������������Ҫ���ص��ֽڵ���Ϣ�����浽Hashtable��
				foreach(XmlNode objNode in objNodeList)
				{

					//����˽ڵ���Ԫ�صĻ������˵�XML�ļ��е�ע����Ϣ��
					if(objNode.NodeType==XmlNodeType.Element)
					{
						strValue=objNode.InnerText.ToString().Trim();
						strName=objNode.Name.ToString().Trim();

						mHst[strName]=strValue;
					}
				}

				return true;

			}
			catch // �������ֱ��catch
			{
				return false;
			}
		}
	}
}
