package main

import (
	"fmt"
	"testing")

func TestGetReturnsZero(t *testing.T) {
	fmt.Println("TestGetReturnsZero")

	var nm NumberManager = &MapNumberManager { }

	verifyGet(t, nm, 0)
}

func TestGet0Get1Get2Get3Release1Get1Get4(t *testing.T) {
	fmt.Println("TestGet0Get1Get2Get3Release1Get1Get4")

	var nm NumberManager = &MapNumberManager { }

	verifyGet(t, nm, 0)
	verifyGet(t, nm, 1)
	verifyGet(t, nm, 2)
	verifyGet(t, nm, 3)
	nm.ReleaseNumber(1)
	verifyGet(t, nm, 1)
	verifyGet(t, nm, 4)
}

func TestGet0Get1Get2Release0Release1Get1Get3(t *testing.T) {
	fmt.Println("TestGet0Get1Get2Release0Release1Get1Get3")

	var nm NumberManager = &MapNumberManager { }

	verifyGet(t, nm, 0)
	verifyGet(t, nm, 1)
	verifyGet(t, nm, 2)
	nm.ReleaseNumber(0)
	nm.ReleaseNumber(1)
	verifyGet(t, nm, 0)
	verifyGet(t, nm, 1)
	verifyGet(t, nm, 3)
}

func TestGet0Get1Get2Release1Release0Get1Get3(t *testing.T) {
	fmt.Println("TestGet0Get1Get2Release1Release0Get1Get3")

	var nm NumberManager = &MapNumberManager { }

	verifyGet(t, nm, 0)
	verifyGet(t, nm, 1)
	verifyGet(t, nm, 2)
	nm.ReleaseNumber(1)
	nm.ReleaseNumber(0)
	verifyGet(t, nm, 0)
	verifyGet(t, nm, 1)
	verifyGet(t, nm, 3)
}

func TestGet0Get1Get2Get3Get4Release1Release3Release2Get1Get2Get3Get5(t *testing.T) {
	fmt.Println("TestGet0Get1Get2Get3Get4Release1Release3Release2Get1Get2Get3Get5")

	var nm NumberManager = &MapNumberManager { }

	verifyGet(t, nm, 0)
	verifyGet(t, nm, 1)
	verifyGet(t, nm, 2)
	verifyGet(t, nm, 3)
	verifyGet(t, nm, 4)

	nm.ReleaseNumber(1)
	nm.ReleaseNumber(3)
	nm.ReleaseNumber(2)

	verifyGet(t, nm, 1)
	verifyGet(t, nm, 2)
	verifyGet(t, nm, 3)
	verifyGet(t, nm, 5)
}

func verifyGet(t *testing.T, nm NumberManager, expected int) {
	actual, err := nm.GetNumber()
	if (err != nil) {
		t.Fatalf("Error from GetNumber(), %v", err)
	}

	if (actual != expected) {
		t.Fatalf("Expected: %d, Actual: %d", expected, actual)
	}
}
