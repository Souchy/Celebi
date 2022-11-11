package espeon.util;

//Copyright 2017 - https://github.com/lbalmaceda
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

//import org.bouncycastle.util.io.pem.PemObject;
//import org.bouncycastle.util.io.pem.PemReader;

import com.souchy.randd.commons.tealwaters.logging.Log;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.security.Key;
import java.security.KeyFactory;
import java.security.KeyPair;
import java.security.KeyPairGenerator;
import java.security.NoSuchAlgorithmException;
import java.security.PrivateKey;
import java.security.PublicKey;
import java.security.interfaces.ECPrivateKey;
import java.security.interfaces.ECPublicKey;
import java.security.spec.EncodedKeySpec;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.PKCS8EncodedKeySpec;
import java.security.spec.X509EncodedKeySpec;
import java.util.Base64;

public class PemUtils {

    /*/
    private static byte[] parsePEMFile(File pemFile) throws IOException {
        if (!pemFile.isFile() || !pemFile.exists()) {
            throw new FileNotFoundException(String.format("The file '%s' doesn't exist.", pemFile.getAbsolutePath()));
        }
        PemReader reader = new PemReader(new FileReader(pemFile));
        PemObject pemObject = reader.readPemObject();

        // KeyPair pair = new JcaPEMKeyConverter().getKeyPair((PEMKeyPair)parsed);

        byte[] content = pemObject.getContent();
        reader.close();
        return content;

        // var content = Files.readString(pemFile.toPath());
        // content = content.replace("-----BEGIN OPENSSH PRIVATE KEY-----",
        // "").replace("-----END OPENSSH PRIVATE KEY-----", "").replace("\n", "");
        // if(content.contains(" ")) content = content.split(" ")[1];
        // Log.info("Key content: [%s]", content);
        // // var decoded = Base64.getDecoder().decode(content);
        // return content.getBytes();
    }

    private static PublicKey getPublicKey(byte[] keyBytes, String algorithm) {
        PublicKey publicKey = null;
        try {
            KeyFactory kf = KeyFactory.getInstance(algorithm);
            EncodedKeySpec keySpec = new X509EncodedKeySpec(keyBytes);
            publicKey = kf.generatePublic(keySpec);
        } catch (NoSuchAlgorithmException e) {
            System.out.println("Could not reconstruct the public key, the given algorithm could not be found.");
        } catch (InvalidKeySpecException e) {
            System.out.println("Could not reconstruct the public key");
        }

        return publicKey;
    }

    private static PrivateKey getPrivateKey(byte[] keyBytes, String algorithm) {
        PrivateKey privateKey = null;
        try {
            KeyFactory kf = KeyFactory.getInstance(algorithm);
            EncodedKeySpec keySpec = new PKCS8EncodedKeySpec(keyBytes);
            privateKey = kf.generatePrivate(keySpec);
        } catch (NoSuchAlgorithmException e) {
            System.out.println("Could not reconstruct the private key, the given algorithm could not be found.");
        } catch (InvalidKeySpecException e) {
            System.out.println("Could not reconstruct the private key");
        }

        return privateKey;
    }

    public static PublicKey readPublicKeyFromFile(String filepath, String algorithm) throws IOException {
        byte[] bytes = PemUtils.parsePEMFile(new File(filepath));
        return PemUtils.getPublicKey(bytes, algorithm);
    }

    public static PrivateKey readPrivateKeyFromFile(String filepath, String algorithm) throws IOException {
        byte[] bytes = PemUtils.parsePEMFile(new File(filepath));
        return PemUtils.getPrivateKey(bytes, algorithm);
    }
    */

    public static void generateKeyPair(String fileName, String algorithm, int size) throws Exception {
        KeyPairGenerator kpg = KeyPairGenerator.getInstance(algorithm);
        // kpg.initialize(2048);
        kpg.initialize(size);
        KeyPair kp = kpg.generateKeyPair();
        Key pub = kp.getPublic();
        Key pvt = kp.getPrivate();

        Files.writeString(Path.of(fileName + ".pub"), Base64.getEncoder().encodeToString(pub.getEncoded()));
        Files.writeString(Path.of(fileName + ""), Base64.getEncoder().encodeToString(pvt.getEncoded()));
    }

    public static PrivateKey readPriv(String filePath, String algorithm) throws Exception {
        var content = Files.readString(Path.of(filePath));
        var bytes = Base64.getDecoder().decode(content);
        
        KeyFactory kf = KeyFactory.getInstance(algorithm);
        var keySpec = new PKCS8EncodedKeySpec(bytes);
        return kf.generatePrivate(keySpec);
    }
    
    public static PublicKey readPub(String filePath, String algorithm) throws Exception {
        var content = Files.readString(Path.of(filePath));
        var bytes = Base64.getDecoder().decode(content);
        
        KeyFactory kf = KeyFactory.getInstance(algorithm);
        var keySpec = new X509EncodedKeySpec(bytes);
        return kf.generatePublic(keySpec);
    }

}
