export class Product {
  public id:                  number;
  public serialNumber:        string;
  public name:                string;
  public description:         string;
  public category:            string;
  public warrantyExpiration:  string;
  public price:               number;
  public currency:            string;

  constructor(product: any = null) {
    this.id                 = product ? product.Id : null;
    this.serialNumber       = product ? product.SerialNumber : '';
    this.name               = product ? product.Name : '';
    this.description        = product ? product.Description : '';
    this.category           = product ? product.Category : '';
    this.warrantyExpiration = product ? product.WarrantyExpiration : '';
    this.price              = product ? product.Price : null;
    this.currency           = product ? product.Currency : '';
  }
}

export class ToDo {
  public id: number;
  public name: string;
  public whenDue: Date;
  public whenCompleted?: Date;
  public completed: boolean;

  constructor(todo: any = null) {
    this.id             = todo ? todo.id : null;
    this.name           = todo ? todo.name : '';
    this.whenDue        = todo ? todo.whenDue : '';
    this.whenCompleted  = todo ? todo.whenCompleted : '';
    this.completed      = todo ? todo.completed : '';
  }
}

export class WeatherForecast {
  public dateFormatted: string;
  public temperatureC: number;
  public temperatureF: number;
  public summary: string;
  
  constructor(temp: any = null) {
    this.dateFormatted    = temp ? temp.dateFormatted : '';
    this.temperatureC     = temp ? temp.temperatureC : 0;
    this.temperatureF     = temp ? temp.temperatureF : 0;
    this.summary          = temp ? temp.summary : '';
  }
}

export class Address {
  public building: string;
  public coord: number[];
  public street: string;
  public zipcode: string;
  
  constructor(address: any = null) {
    this.building = address ? address.building : '';
    this.coord    = address ? address.coord : new Array<number>();
    this.street   = address ? address.street : '';
    this.zipcode  = address ? address.zipcode : '';
  }
}

export class Rating {
  public date: string;
  public grade: string;
  public score: number;
  
  constructor(grade: any = null) {
    this.date   = grade ? grade.date : '';
    this.grade  = grade ? grade.grade : '';
    this.score  = grade ? grade.score : 0;
  }
}

export class Restaurant {
  public address: Address;
  public borough: string;
  public cuisine: string;
  public grades: Rating[];
  public name: string;
  public restaurant_id: number
  
  constructor(resturant: any = null) {
    this.address        = resturant ? new Address(resturant.address) : null;
    this.borough        = resturant ? resturant.borough : '';
    this.cuisine        = resturant ? resturant.cuisine : '';
    this.grades         = resturant && resturant.grades ? resturant.grades.map(grade => new Rating(grade)) : new Array<Rating>();
    this.name           = resturant ? resturant.name : '';
    this.restaurant_id  = resturant ? parseInt(resturant.restaurant_id) : 0;
  }
}

export class RestaurantModel {
  public totalRestaurants: number;
  public pageInstances: Array<Restaurant>;
  public currentPage: number;
  public pageSize: number;
  public numberPages: number;
  public previousPage: number;
  public showPreviousPage: boolean;
  public nextPage: number;
  public showNextPage: boolean;
  
  constructor(page: any = null) {
    this.totalRestaurants   = page ? page.totalRestaurants : 0;
    this.pageInstances      = page && page.pageInstances ? page.pageInstances.map(rest => new Restaurant(rest)) : [];
    this.currentPage        = page ? page.currentPage : -1;
    this.pageSize           = page ? page.pageSize : 0;
    this.numberPages        = page ? page.numberPages : 0;
    this.previousPage       = page ? page.previousPage : 0;
    this.showPreviousPage   = page ? page.showPreviousPage : false;
    this.nextPage           = page ? page.nextPage : 0;
    this.showNextPage       = page ? page.showNextPage : false;
  }
}