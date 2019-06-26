import { Injectable } from '@angular/core';
import { Http, Request, RequestOptions, RequestMethod, Headers } from '@angular/http';
import { RestaurantModel } from '../models/interfaces';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'

@Injectable()
export class MongoDataService {
    _http : Http;

    constructor(http: Http) {
        this._http = http;
    }

    getMongoPage(page: number) : Observable<RestaurantModel> {
        var url = '/api/Restaurants';

        if (page) {
            url = url + '?page=' + page;
        }

        return this._http.get(url).map(result => {
            return result.json() as RestaurantModel;
        });
    }
}