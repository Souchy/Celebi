package sylveon;

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

import java.util.ArrayList;
import java.util.List;

class SylveonTest {
    
    @Test 
    void appHasAGreeting() {
        // Espeon classUnderTest = new Espeon();
        // assertNotNull(classUnderTest.getGreeting(), "app should have a greeting");
    }

    @Test
    void testListLoopAdd() {
        List<String> list = new ArrayList<>();
        for(int i = 0; i < 5; i++) {
            list.add("a");
        }
        for(int i = 0; i < list.size(); i++) {
            if(i == 1 || i == 2) {
                list.add(i+1, "b");
                list.add(i+2, "c");
            }
            System.out.println("val : " + list.get(i));
        }
    }
    
}
