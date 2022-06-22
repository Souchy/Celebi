#pragma once


#include <string>
#include <vector>
#include "Effect.h"
using namespace std;

class Damage : public Effect {

protected:
    
private:

public:
	Damage();
	~Damage();

    string type;
    int power;

};

