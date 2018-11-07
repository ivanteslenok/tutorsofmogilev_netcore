import React from 'react';
import EditableListItem from './Item';

import './index.css';

export default function EditableList({
  items,
  handleItemEdit,
  handleItemDelete
}) {
  return (
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
}
