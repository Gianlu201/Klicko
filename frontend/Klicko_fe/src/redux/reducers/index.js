import { LOGOUT, SET_LOGGED_USER } from '../actions';

const initialState = {
  profile: {},
  cart: {},
};

const mainReducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_LOGGED_USER:
      return {
        ...state,
        profile: action.payload,
      };

    case LOGOUT:
      return {
        ...state,
        profile: {},
      };

    default:
      return state;
  }
};

export default mainReducer;
