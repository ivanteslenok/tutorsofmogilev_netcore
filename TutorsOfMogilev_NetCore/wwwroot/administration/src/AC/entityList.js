import {
  LOAD_LIST,
  CREATE_ITEM,
  UPDATE_ITEM,
  REMOVE_ITEM
} from '../constants';

export default function entityList(entityName, entityUrl) {
  return {
    loadEntities: () => ({
      type: entityName + LOAD_LIST,
      get: entityUrl
    }),
    createEntity: entity => ({
      type: entityName + CREATE_ITEM,
      payload: { data: entity },
      post: entityUrl
    }),
    updateEntity: (id, entity) => ({
      type: entityName + UPDATE_ITEM,
      payload: { id, data: entity },
      put: entityUrl
    }),
    removeEntity: id => ({
      type: entityName + REMOVE_ITEM,
      payload: { id },
      del: entityUrl
    })
  }
}