import {
  CART_MODIFIED,
  LOGOUT,
  SET_LOGGED_USER,
  SET_USER_CART,
} from '../actions';

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
        cart: {},
      };

    case SET_USER_CART:
      return {
        ...state,
        cart: action.payload,
      };

    case CART_MODIFIED:
      return {
        ...state,
        cart: {
          ...state.cart,
          modified: true,
        },
      };

    default:
      return state;
  }
};

export default mainReducer;
