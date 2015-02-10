// stdafx.cpp : source file that includes just the standard includes
// SOS.Clients.CheckSumConsole.pch will be the pre-compiled header
// stdafx.obj will contain the pre-compiled type information

#include "stdafx.h"

// TODO: reference any additional headers you need in STDAFX.H
// and not in this file


unsigned char calc_checksum(const char *s)
{
	/** Initialize. */
	unsigned char result;
	result = 0;

	/** Skip dollar sign. */
	s++;

	/** Bitwise operator. */
	while ((*s != '*') && (*s != '\0'))
	{
		result ^= *s++;
	}

	/** Return result. */
	return result;
}