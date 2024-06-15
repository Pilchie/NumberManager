use std::collections::BinaryHeap;
use std::cmp::Reverse;

pub trait NumberManager {
    fn get_number(&mut self) -> i32;
    fn release_number(&mut self, number: i32);
}

pub struct BinaryHeapNumberManager {
    heap: BinaryHeap<Reverse<i32>>,
    max: i32,
}

impl BinaryHeapNumberManager {
    pub fn new() -> Self {
        BinaryHeapNumberManager {
            heap: BinaryHeap::new(),
            max: 0,
        }
    }
}

impl NumberManager for BinaryHeapNumberManager {
    fn get_number(&mut self) -> i32 {
        if let Some(Reverse(number)) = self.heap.pop() {
            number
        } else {
            self.max += 1;
            self.max
        }
    }

    fn release_number(&mut self, number: i32) {
        if number == self.max {
            self.max -= 1;
        } else {
            self.heap.push(Reverse(number));
        }
    }
}
