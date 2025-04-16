import {
  CART_MODIFIED,
  EMPTY_CART,
  LOGOUT,
  SET_CATEGORIES_LIST,
  SET_EXPERIENCES_LIST,
  SET_LOGGED_USER,
  SET_USER_CART,
} from '../actions';

const initialState = {
  profile: {},
  cart: {},
  experiences: [],
  categories: [],
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

    case EMPTY_CART:
      return {
        ...state,
        cart: {
          ...state.cart,
          experiences: [],
        },
      };

    case CART_MODIFIED:
      return {
        ...state,
        cart: {
          ...state.cart,
          modified: true,
        },
      };

    case SET_EXPERIENCES_LIST:
      return {
        ...state,
        experiences: action.payload,
      };

    case SET_CATEGORIES_LIST:
      return {
        ...state,
        categories: action.payload,
      };

    default:
      return state;
  }
};

export default mainReducer;
