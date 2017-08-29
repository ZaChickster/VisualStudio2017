import * as actions from '../actions/products.action';
import { RestaurantModel }  from '../../models';

export interface State {
  loading: boolean;
  loaded:  boolean;
  failed:  boolean;
  data:    RestaurantModel;
};

const INITIAL_STATE: State = {
  loading: false,
  loaded:  false,
  failed:  false,
  data:    new RestaurantModel()
};

export function reducer(state = INITIAL_STATE, action: actions.Actions): State {
  if (!action) return state;

  switch (action.type) {
    case actions.ActionTypes.LOAD: {
      return Object.assign({}, state, {
        loading: true
      });
    }

    case actions.ActionTypes.LOAD_SUCCESS: {
      return Object.assign({}, state, {
        loaded:   true,
        loading:  false,
        failed:   false,
        data:     action.payload
      });
    }

    case actions.ActionTypes.LOAD_FAIL: {
      return Object.assign({}, state, {
        loaded:   false,
        loading:  false,
        failed:   true,
        data:     new RestaurantModel()
      });
    }

    default: {
      return state;
    }
  }
};

export const getData    = (state: State) => {
  return state.data
};
export const getLoading = (state: State) => state.loading;
export const getLoaded  = (state: State) => state.loaded;
export const getFailed  = (state: State) => state.failed;