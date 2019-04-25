import _ from 'lodash';

import React, { useState, useEffect } from 'react';
import Select from '@material-ui/core/Select';
import Input from '@material-ui/core/Input';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import FormControl from '@material-ui/core/FormControl';
import Chip from '@material-ui/core/Chip';
import SaveBtn from './SaveBtn';
import CancelBtn from './CancelBtn';

const MultipleSelectEditor = props => {
  const [currentValues, setCurrentValues] = useState([]);
  const [hasChanges, setHasChanges] = useState(false);

  useEffect(() => {
    const { loading, loaded, loadData, currentValues } = props;

    setCurrentValues(currentValues);

    if (!loaded && !loading) loadData();
  });

  const detectChanges = () => {
    const fromState = new Set(currentValues.map(x => x.id));
    const fromProps = new Set(props.currentValues.map(x => x.id));

    setHasChanges(!_.isEqual(fromState, fromProps));
  };

  const handleChange = event => {
    setCurrentValues(event.target.value);
    detectChanges();
  };

  const handleDelete = id => {
    setCurrentValues(currentValues.filter(x => x.id !== id));
    setHasChanges(true);
    detectChanges();
  };

  const saveChanges = () => {
    const { editId, update } = props;
    let fromState = currentValues.map(x => x.id);
    let fromProps = props.currentValues.map(x => x.id);

    const added = fromState.filter(x => !fromProps.includes(x));
    const deleted = fromProps.filter(x => !fromState.includes(x));

    update(editId, {
      added,
      deleted
    });

    setHasChanges(false);
  };

  const cancelChanges = () => {
    setCurrentValues(props.currentValues);
    setHasChanges(false);
  };

  const { label, availableValues, loading } = props;

  const value = availableValues.filter(x =>
    currentValues.find(y => y.id === x.id)
  );

  const content = loading ? (
    'Loading...'
  ) : (
    <div style={{ display: 'flex', paddingTop: '10px' }}>
      <Select
        style={{ minWidth: '150px', maxWidth: '600px' }}
        multiple
        value={value}
        onChange={handleChange}
        input={<Input />}
        renderValue={selected => (
          <div style={{ display: 'flex', flexWrap: 'wrap' }}>
            {selected.map(x => (
              <Chip
                key={x.id}
                label={x.name}
                style={{ margin: '2px' }}
                onDelete={() => handleDelete(x.id)}
              />
            ))}
          </div>
        )}
      >
        {availableValues.map(x => (
          <MenuItem key={x.id} value={x}>
            {x.name}
          </MenuItem>
        ))}
      </Select>
      {hasChanges && (
        <div>
          <SaveBtn onExecute={saveChanges} />
          <CancelBtn onExecute={cancelChanges} />
        </div>
      )}
    </div>
  );

  return (
    <FormControl style={{ minWidth: 300, maxWidth: 500 }}>
      <InputLabel>{label}</InputLabel>
      {content}
    </FormControl>
  );
};

export default MultipleSelectEditor;
