use number_manager::{BinaryHeapNumberManager, NumberManager};

fn main() {
    let mut mgr = BinaryHeapNumberManager::new();
    println!("{}", mgr.get_number());
    println!("{}", mgr.get_number());
    mgr.release_number(1);
    println!("{}", mgr.get_number());
    println!("{}", mgr.get_number());
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_binary_heap_number_manager() {
        let mut mgr = BinaryHeapNumberManager::new();
        assert_eq!(mgr.get_number(), 1);
        assert_eq!(mgr.get_number(), 2);
        mgr.release_number(2);
        assert_eq!(mgr.get_number(), 2);

        mgr.release_number(1);
        assert_eq!(mgr.get_number(), 1);
        assert_eq!(mgr.get_number(), 3);
        assert_eq!(mgr.get_number(), 4);
    }
}
