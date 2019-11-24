package main

import (
	"errors"
	"math")

type NumberManager interface {
	GetNumber() (int, error)
	ReleaseNumber(number int)
}

type MapNumberManager struct {
	numbers map[int]bool
}

func (nm *MapNumberManager) GetNumber() (int, error) {
	if (nm.numbers == nil) {
		nm.numbers = make(map[int]bool)
	}
	for i := 0; i < math.MaxInt32; i++ {
		if !nm.numbers[i] {
			nm.numbers[i] = true
			return i, nil
		}
	}

	return -1, errors.New("error")
}

func (nm *MapNumberManager) ReleaseNumber(number int) {
	nm.numbers[number] = false
}