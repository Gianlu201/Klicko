import {
  ADD_EXPERIENCE_TO_LOCAL_CART,
  ADD_EXPERIENCE_UNIT_TO_LOCAL_CART,
  CART_MODIFIED,
  EMPTY_CART,
  LOGOUT,
  REMOVE_EXPERIENCE_FROM_LOCAL_CART,
  REMOVE_EXPERIENCE_UNIT_FROM_LOCAL_CART,
  SET_CART_FROM_LOCAL,
  SET_LOGGED_USER,
  SET_SEARCHBAR_QUERY,
  SET_SELECTED_CATEGORY,
  SET_USER_CART,
  SET_USER_FIDELITY_CARD,
} from '../actions';

const initialState = {
  profile: {},
  cart: {
    cartId: '',
    experiences: [],
  },
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
      return initialState;

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

    case SET_CART_FROM_LOCAL: {
      return {
        ...state,
        cart: {
          ...state.cart,
          experiences: action.payload.experiences,
        },
      };
    }

    case ADD_EXPERIENCE_TO_LOCAL_CART: {
      let experiencesList = [];
      state.cart.experiences.forEach((exp) => {
        if (exp.experienceId === action.payload.experienceId) {
          state.cart.experiences.forEach((element) => {
            if (element.experienceId === action.payload.experienceId) {
              experiencesList.push({
                ...element,
                quantity: element.quantity + 1,
              });
            } else {
              experiencesList.push(element);
            }
          });
        }
      });

      if (experiencesList.length === 0) {
        return {
          ...state,
          cart: {
            ...state.cart,
            experiences: [...state.cart.experiences, action.payload],
          },
        };
      } else {
        return {
          ...state,
          cart: {
            ...state.cart,
            experiences: experiencesList,
          },
        };
      }
    }

    case REMOVE_EXPERIENCE_FROM_LOCAL_CART: {
      let experiencesList = [];

      state.cart.experiences.forEach((exp) => {
        if (exp.experienceId !== action.payload) {
          experiencesList.push(exp);
        }
      });

      return {
        ...state,
        cart: {
          ...state.cart,
          experiences: experiencesList,
        },
      };
    }

    case ADD_EXPERIENCE_UNIT_TO_LOCAL_CART: {
      let experiencesList = [];
      state.cart.experiences.forEach((exp) => {
        if (exp.experienceId === action.payload) {
          experiencesList.push({ ...exp, quantity: exp.quantity + 1 });
        } else {
          experiencesList.push(exp);
        }
      });

      return {
        ...state,
        cart: {
          ...state.cart,
          experiences: experiencesList,
        },
      };
    }

    case REMOVE_EXPERIENCE_UNIT_FROM_LOCAL_CART: {
      let experiencesList = [];
      state.cart.experiences.forEach((exp) => {
        if (exp.experienceId === action.payload) {
          if (exp.quantity - 1 > 0) {
            experiencesList.push({ ...exp, quantity: exp.quantity - 1 });
          }
        } else {
          experiencesList.push(exp);
        }
      });

      return {
        ...state,
        cart: {
          ...state.cart,
          experiences: experiencesList,
        },
      };
    }

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
