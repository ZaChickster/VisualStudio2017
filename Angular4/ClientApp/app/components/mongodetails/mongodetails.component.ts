import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Restaurant, MongoDetailModel } from '../../models/interfaces';
import { MongoDataService } from '../../services/mongodata.service';

@Component({
    selector: 'mongodetails',
    templateUrl: './mongodetails.component.html'
})
export class MongoDetailsComponent {
    model: MongoDetailModel;
    _service: MongoDataService;
    _route: ActivatedRoute;

    constructor(mongo: MongoDataService, route: ActivatedRoute) {
        this.model = {} as MongoDetailModel;
        this._service = mongo;
        this._route = route;

        route.params.subscribe(params => {
            this.updateModel(params['id']);
        });
    }

    updateModel(id: string) {
        if (id) {
            this._service.getSingleModel(id).subscribe(result => {
                this.model = result;
            });
        } else {
            this.model.data = {} as Restaurant;
        }        
    }

    doSubmit() {
        this._service.saveSingleModel(this.model.data).subscribe(result => {
            this.model = result;
        });        
    }

    doDelete() {
        this._service.saveSingleModel(this.model.data).subscribe(result => {
            this.model = result;
            this.goBack();
        });    
    }

    goBack() {
        window.history.back();
    }
}