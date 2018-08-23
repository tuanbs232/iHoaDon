using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Bkav.eHoadon.XML.eHoadon.Signning
{
    public class eHoadonCert
    {
        /// <summary>
        /// Hiển thị giao diện lựa chon Certificate từ Keystore
        /// </summary>
        /// <returns>Chứng thư số</returns>
        public static X509Certificate2 GetCertificate()
        {
            X509Store certStore = new X509Store(StoreLocation.CurrentUser);
            certStore.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certs = X509Certificate2UI.SelectFromCollection(
                    certStore.Certificates,
                    "Chọn chứng thư số",
                    "Chọn chứng thư số",
                    X509SelectionFlag.SingleSelection);
            return certs.Count > 0 ? certs[0] : null;
        }

        public static X509Certificate2 GetCertificate(string selectedCertSerialNumber)
        {
            X509Store certStore = new X509Store(StoreLocation.CurrentUser);
            certStore.Open(OpenFlags.ReadOnly);

            if (String.IsNullOrEmpty(selectedCertSerialNumber))
                throw new Exception("Chưa nhập thông tin số serial chứng thư số");

            X509Certificate2 selectedCert = null;

            foreach (X509Certificate2 certificate2 in certStore.Certificates)
            {
                if (certificate2.SerialNumber.ToUpper().Trim().CompareTo(selectedCertSerialNumber.ToUpper().Trim()) == 0)
                {
                    selectedCert = certificate2;
                    break;
                }
            }

            if (selectedCert == null)
                throw new Exception("Không tìm thấy chứng thư số trong hệ thống");
            return selectedCert;
        }
    }
}
