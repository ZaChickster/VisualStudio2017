export interface ToDo {
    id: number;
    name: string;
    whenDue: Date;
    whenCompleted?: Date;
    completed: boolean
}

export interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

export interface Address {
    building: string;
    coord: number[];
    street: string;
    zipcode: string;
}

export interface Rating {
    date: string;
    grade: string;
    score: number;
}

export interface Restaurant {
    address: Address;
    borough: string;
    cuisine: string;
    grades: Rating[];
    name: string;
    restaurant_id: string
}

export interface RestaurantModel {
    totalRestaurants: number;
    pageInstances: Restaurant[];
    currentPage: number;
    pageSize: number;
    numberPages: number;
    previousPage: number;
    showPreviousPage: boolean;
    nextPage: number;
    showNextPage: boolean;
}

export interface MongoDetailModel {
    data: Restaurant;
    message: string;
}