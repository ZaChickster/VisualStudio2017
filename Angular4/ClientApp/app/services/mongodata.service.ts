import { Injectable } from '@angular/core';
import { Http, Request, Response, RequestOptions, RequestMethod, Headers } from '@angular/http';
import { RestaurantModel, MongoDetailModel, Restaurant } from '../models/interfaces';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'

@Injectable()
export class MongoDataService {
    _http : Http;
    _listUrl: string = '/api/Restaurants';
    _singleUrl: string = '/api/Restaurant';

    constructor(http: Http) {
        this._http = http;
    }

    getPage(page: number) : Observable<RestaurantModel> {
        const  url = `${this._listUrl}?page=${page}`;

        return this._http.get(url).map(result => {
            return result.json() as RestaurantModel;
        });
    }

    getSingleModel(id : string) : Observable<MongoDetailModel> {
        const url = `${this._singleUrl}?id=${id}`;

        return this._http.get(url).map(result => {
            return {
                data: result.json() as Restaurant,
                message: ''
            };
        });
    }

    saveSingleModel(data : Restaurant) : Observable<MongoDetailModel> {
        let method : RequestMethod;
        let operation: string;

        if (data.restaurant_id) {
            method = RequestMethod.Post;
            operation = 'Update';
        } else {
            method = RequestMethod.Put;
            operation = 'Create'
        }

        return this.performCrud(method, data).map(result => {
            return {
                data: result.json() as Restaurant,
                message: `${operation} Successful`
            };
        });        
    }

    deleteSingleModel(data : Restaurant) : Observable<MongoDetailModel> {
        return this.performCrud(RequestMethod.Delete, data).map(result => {
            return {
                data: {} as Restaurant,
                message: 'Delete Successful'
            };
        });
    }

    private performCrud(method : RequestMethod, data : Restaurant) : Observable<Response> {
        const headers = new Headers();

        headers.append('Content-Type', 'application/json');

        const reqOpts = new RequestOptions({
            method: method,
            url: this._singleUrl,
            headers: headers,
            body: JSON.stringify(data)
        });

        return this._http.request(new Request(reqOpts));
    }
}