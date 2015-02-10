// SOS.Clients.CheckSumConsole.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"


int _tmain(int argc, _TCHAR* argv[])
{
	unsigned char checksum;
	checksum = calc_checksum("$GPRMC,235947.000,V,0000.0000,N,00000.0000,E,,,041299,,*");

	printf("Checksum = %02X\n", checksum);
	return 0;
}

