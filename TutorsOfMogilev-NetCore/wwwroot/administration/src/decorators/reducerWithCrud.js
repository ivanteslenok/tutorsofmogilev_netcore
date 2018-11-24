import {
  LOAD_LIST,
  CREATE_ITEM,
  UPDATE_ITEM,
  REMOVE_ITEM,
  START,
  SUCCESS,
  FAIL
} from '../constants';

export default function reducerWithCrud(entityName) {
  const initialState = {
    items: [],
    loading: false,
    loaded: false
  };

  return (state = initialState, action) => {
    const { type, payload, data } = action;

    switch (type) {
      case entityName + LOAD_LIST + START:
      case entityName + CREATE_ITEM + START:
      case entityName + UPDATE_ITEM + START:
      case entityName + REMOVE_ITEM + START:
        return { ...state, loading: true };

      case entityName + LOAD_LIST + SUCCESS:
        return { ...state, items: data, loading: false, loaded: true };

      case entityName + CREATE_ITEM + SUCCESS:
        const itemsAfterCreate = [...state.items, data];
        return { ...state, items: itemsAfterCreate, loading: false };

      case entityName + UPDATE_ITEM + SUCCESS:
        const itemsAfterUpdate = state.items.map(
          item => (item.id === payload.id ? { ...item, ...data } : item)
        );
        return { ...state, items: itemsAfterUpdate, loading: false };

      case entityName + REMOVE_ITEM + SUCCESS:
        const itemsAfterRemove = state.items.filter(
          item => item.id !== payload.id
        );
        return { ...state, items: itemsAfterRemove, loading: false };

      case entityName + LOAD_LIST + FAIL:
      case entityName + CREATE_ITEM + FAIL:
      case entityName + UPDATE_ITEM + FAIL:
      case entityName + REMOVE_ITEM + FAIL:
        return { ...state, loading: false, loaded: false };

      default:
        return state;
    }
  };
}
