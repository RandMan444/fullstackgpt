using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.Text;

namespace ChatGPTAPI
{
    public class AmazonUploader
    {
        public bool sendMyFileToS3(string websiteContent)
        {
            string tempFilePath = "C:\\temp\\index.html";
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }

            using (FileStream fs = File.Create(tempFilePath))
            {
                Byte[] content = new UTF8Encoding(true).GetBytes(websiteContent);
                fs.Write(content, 0, content.Length);
            }

            string bucketName = "{YOURS3BUCKETNAME}";

            var chain = new CredentialProfileStoreChain();
            AWSCredentials awsCredentials;

            bool isDev = chain.TryGetAWSCredentials("{YOURS3AWSCREDENTIAL}", out awsCredentials);
            AmazonS3Client s3Client = new AmazonS3Client(awsCredentials, RegionEndpoint.USWest2);

            TransferUtility utility = new TransferUtility(s3Client);
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

    
            request.BucketName = bucketName;
            
            request.Key = "index.html";
            request.FilePath = tempFilePath;
            utility.Upload(request);

            return true;
        }
    }

}
