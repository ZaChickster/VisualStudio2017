import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface RestaurantState {
    isLoading: boolean;
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
    restaurant_id: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestRestaurantsAction {
    type: 'REQUEST_RESTAURANTS';
    currentPage: number;
}

interface ReceiveRestaurantsAction {
    type: 'RECEIVE_RESTAURANTS';
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

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestRestaurantsAction | ReceiveRestaurantsAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestRestaurants: (currentPage: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        if (currentPage !== getState().restaurants.currentPage) {
            let fetchTask = fetch(`/api/Restaurants?page=${currentPage}`)
                .then(response => response.json() as Promise<RestaurantState>)
                .then(data => {
                    dispatch({
                        type: 'RECEIVE_RESTAURANTS', 
                        totalRestaurants: data.totalRestaurants,
                        pageInstances: data.pageInstances,
                        currentPage: data.currentPage,
                        pageSize: data.pageSize,
                        numberPages: data.numberPages,
                        previousPage: data.previousPage,
                        showPreviousPage: data.showPreviousPage,
                        nextPage: data.nextPage,
                        showNextPage: data.showNextPage
                    });
                });

            addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
            dispatch({
                type: 'REQUEST_RESTAURANTS',
                currentPage: currentPage
            });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.
const unloadedState: RestaurantState = {
    isLoading: false,
    totalRestaurants: null,
    pageInstances: [],
    currentPage: null,
    pageSize: 15,
    numberPages: null,
    previousPage: null,
    showPreviousPage: false,
    nextPage: null,
    showNextPage: true
};

export const reducer: Reducer<RestaurantState> = (state: RestaurantState, action: KnownAction) => {
    switch (action.type) {
        case 'REQUEST_RESTAURANTS':
            return {
                isLoading: true,
                totalRestaurants: state.totalRestaurants,
                pageInstances: state.pageInstances,
                currentPage: action.currentPage,
                pageSize: state.pageSize,
                numberPages: state.numberPages,
                previousPage: state.previousPage,
                showPreviousPage: state.showPreviousPage,
                nextPage: state.nextPage,
                showNextPage: state.showNextPage
            };
        case 'RECEIVE_RESTAURANTS':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            if (action.currentPage === state.currentPage) {
                return {
                    isLoading: false,
                    totalRestaurants: action.totalRestaurants,
                    pageInstances: action.pageInstances,
                    currentPage: action.currentPage,
                    pageSize: action.pageSize,
                    numberPages: action.numberPages,
                    previousPage: action.previousPage,
                    showPreviousPage: action.showPreviousPage,
                    nextPage: action.nextPage,
                    showNextPage: action.showNextPage
                };
            }
            break;
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};
