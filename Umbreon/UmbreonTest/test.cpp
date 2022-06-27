#include "pch.h"

#include "../src/models/Effect.h"
#include "../src/models/Aoe.h"
#include "../src/models/Target.h"

#include <string>
#include <iostream>

TEST(TestCaseName, TestName) {
	EXPECT_EQ(1, 1);
	EXPECT_TRUE(true);
}


TEST(TestEffect, CTor) {
	Effect e;

}


TEST(TestAoe, Print) {
	// create aoe
	Aoe aoe(5, 5);
	// fill aoe with 7's
	for (int i = 0; i < aoe.height; i++) {
		aoe.set(i, 2, 6);
		aoe.set(2, i, 6);
	}
	// print
	aoe.print();
	std::cout << "" << std::endl;

	// moves
	//aoe.addColumn(0);
	//aoe.addRow(3);
	//aoe.resize(6, 5);
	//aoe.removeRow(2);
	//aoe.removeColumn(1);
	//aoe.addRow(3);
	//aoe.removeRow(3);
	//aoe.move(-1, -1);
	//aoe.move(1, 1);

	//Aoe aoe2(3, 3, 5);
	//aoe2.fill(4);
	//aoe.setAoe(aoe2, 3, 3);

	TargetTypeFilter arr[1][3] = {
		//{ 1, 1, 1 },
		//{ 1, 1, 1 },
		{ 1, 1, 1 }
	};
	Aoe aoe3(1, 3, &arr[0][0]);
	aoe.setAoe(aoe3);

	// print
	aoe.print();
	std::cout << "" << std::endl;

	SUCCEED() << "Hello succeed text";
	EXPECT_EQ(1, 2);
}

TEST(TestEffect, Deserialize) {
	Effect e;

}