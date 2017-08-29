import { Action }                     from '@ngrx/store';
import { Restaurant }                 from '../../models';
import { RestaurantModel }            from '../../models';
import { type }                       from '../../utility';

export const ActionTypes = {
  LOAD:         type('[Products] Load'),
  LOAD_SUCCESS: type('[Products] Load Success'),
  LOAD_FAIL:    type('[Products] Load Fail')
};

/**
 * Product Actions
 */
export class LoadAction implements Action {
  type = ActionTypes.LOAD;

  constructor(public payload: number = null) { }
}

export class LoadSuccessAction implements Action {
  type = ActionTypes.LOAD_SUCCESS;

  constructor(public payload: RestaurantModel = null) { }
}

export class LoadFailAction implements Action {
  type = ActionTypes.LOAD_FAIL;

  constructor(public payload: any = null) { }
}

export type Actions
  = LoadAction
  | LoadSuccessAction
  | LoadFailAction;