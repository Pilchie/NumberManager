use actix_web::{get, post, web, App, HttpResponse, HttpServer, Responder};
use number_manager::{BinaryHeapNumberManager, NumberManager};
use std::sync::Mutex;

struct AppState {
    mgr: Mutex<BinaryHeapNumberManager>,
}

#[get("/")]
async fn index() -> impl Responder {
    HttpResponse::Ok().body("Try /get_number or /release_number")
}

#[get("/get_number")]
async fn get_number(data: web::Data<AppState>) -> impl Responder {
    let mut mgr = data.mgr.lock().unwrap();
    HttpResponse::Ok().body(format!("{}\n", mgr.get_number()))

}

#[post("/release_number")]
async fn release_number(data: web::Data<AppState>, number: web::Json<i32>) -> impl Responder {
    let mut mgr = data.mgr.lock().unwrap();
    mgr.release_number(number.into_inner());
    HttpResponse::Ok().finish()
}

#[actix_web::main]
async fn main() -> std::io::Result<()> {
    let mgr = web::Data::new(AppState {
        mgr: Mutex::new(BinaryHeapNumberManager::new()),
    });
    HttpServer::new(move || {
        App::new()
            .app_data(mgr.clone())
            .service(get_number)
            .service(release_number)
    })
    .bind(("localhost", 8080))?
    .run()
    .await
}