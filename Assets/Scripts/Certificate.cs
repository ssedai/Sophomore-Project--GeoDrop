using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class Certificate : MonoBehaviour {

	IEnumerator Start () 
	{
		
		ServicePointManager.ServerCertificateValidationCallback = TrustCertificate;


		HttpWebRequest request = (HttpWebRequest) WebRequest.Create( "https://ilta.oakland.edu/" );
		HttpWebResponse response = (HttpWebResponse) request.GetResponse();

		Stream dataStream = response.GetResponseStream ();
		StreamReader reader = new StreamReader (dataStream);
		string responseFromServer = reader.ReadToEnd ();

		Debug.Log ("responseFromServer=" + responseFromServer );

		yield return 0;
	}
	//http://stackoverflow.com/questions/3674692/mono-webclient-invalid-ssl-certificates
	private static bool TrustCertificate(object sender, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors)
	{
		// all Certificates are accepted
		return true;
	}
}
