import {
  CART_MODIFIED,
  EMPTY_CART,
  LOGOUT,
  SET_LOGGED_USER,
  SET_SEARCHBAR_QUERY,
  SET_SELECTED_CATEGORY,
  SET_USER_CART,
  SET_USER_FIDELITY_CARD,
} from '../actions';

const initialState = {
  profile: {},
  cart: {},
  fidelityCard: {},
  searchBarQuery: '',
  selectedCategoryName: '',
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

    case SET_USER_FIDELITY_CARD:
      return {
        ...state,
        fidelityCard: action.payload,
      };

    case SET_SEARCHBAR_QUERY:
      return {
        ...state,
        searchBarQuery: action.payload,
      };

    case SET_SELECTED_CATEGORY:
      return {
        ...state,
        selectedCategoryName: action.payload,
      };

    default:
      return state;
  }
};

export default mainReducer;
