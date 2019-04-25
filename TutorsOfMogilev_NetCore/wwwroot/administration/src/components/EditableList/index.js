import React from 'react';
import EditableListItem from './Item';

import './index.css';

const EditableList = ({ items, handleItemEdit, handleItemDelete }) => (
  <ul className="editable-list">
    {items.map(item => (
      <EditableListItem
        key={item.id}
        item={item}
        handleEdit={() => handleItemEdit(item.id)}
        handleDelete={() => handleItemDelete(item.id)}
      />
    ))}
  </ul>
);

export default EditableList;
