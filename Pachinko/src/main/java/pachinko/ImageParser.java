package pachinko;

import java.nio.ByteBuffer;
import java.nio.IntBuffer;

import javax.imageio.ImageIO;

import org.lwjgl.glfw.GLFW;
import org.lwjgl.system.MemoryStack;
import java.awt.image.BufferedImage;
import java.io.ByteArrayOutputStream;
import java.io.File;

public class ImageParser {
    public ByteBuffer get_image() {
        return image;
    }

    public int get_width() {
        return width;
    }

    public int get_heigh() {
        return heigh;
    }

    private ByteBuffer image;
    private int width, heigh;

    ImageParser(int width, int heigh, ByteBuffer image) {
        this.image = image;
        this.heigh = heigh;
        this.width = width;
    }
    public static ImageParser load_image(String path) {
        ByteBuffer image = null;
        int width = 0, height = 0;
        try {// try (MemoryStack stack = MemoryStack.stackPush()) {
            // IntBuffer comp = stack.mallocInt(1);
            // IntBuffer w = stack.mallocInt(1);
            // IntBuffer h = stack.mallocInt(1);
                
            BufferedImage originalImage = ImageIO.read(new File(path));
            width = originalImage.getWidth();
            height = originalImage.getHeight();
            ByteArrayOutputStream baos = new ByteArrayOutputStream();
            ImageIO.write(originalImage, "png", baos);
            baos.flush();
            byte[] imageInByte = baos.toByteArray();
            baos.close();
            ByteBuffer buf = ByteBuffer.wrap(imageInByte);
            image = buf;

            // image = org.lwjgl.stb.STBImage.stbi_load(path, w, h, comp, 4);
            if (image == null) {
                // throw new resource_error("Could not load image resources.");
            }
            // width = w.get();
            // heigh = h.get();
        } catch(Exception e) {
            e.printStackTrace();
        }
        return new ImageParser(width, height, image);
    }

    private static void loadimage2(String path, IntBuffer w, IntBuffer h) throws Exception {

    }
}