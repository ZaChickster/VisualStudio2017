export interface ToDo {
    id: number;
    name: string;
    whenDue: Date;
    whenCompleted: Date;
    completed: boolean
}

export interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}