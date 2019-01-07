import _ from 'lodash';

import React, { Component } from 'react';
import Select from '@material-ui/core/Select';
import Input from '@material-ui/core/Input';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import FormControl from '@material-ui/core/FormControl';
import Chip from '@material-ui/core/Chip';
import SaveBtn from './SaveBtn';
import CancelBtn from './CancelBtn';

export default class MultipleSelectEditor extends Component {
  state = {
    currentValues: [],
    hasChanges: false
  };

  componentDidMount() {
    const { loading, loaded, loadData, currentValues } = this.props;

    this.setState({ currentValues: currentValues });

    if (!loaded && !loading) loadData();
  }

  setHasChanges = () => {
    const fromState = new Set(this.state.currentValues.map(x => x.id));
    const fromProps = new Set(this.props.currentValues.map(x => x.id));

    if (!_.isEqual(fromState, fromProps)) {
      this.setState({ hasChanges: true });
    } else {
      this.setState({ hasChanges: false });
    }
  };

  handleChange = event => {
    this.setState({ currentValues: event.target.value }, this.setHasChanges);
  };

  handleDelete = id => {
    const { currentValues } = this.state;
    this.setState(
      {
        currentValues: currentValues.filter(x => x.id !== id),
        hasChanges: true
      },
      this.setHasChanges
    );
  };

  saveChanges = () => {
    const { editId, update } = this.props;
    let fromState = this.state.currentValues.map(x => x.id);
    let fromProps = this.props.currentValues.map(x => x.id);

    const added = fromState.filter(x => !fromProps.includes(x));
    const deleted = fromProps.filter(x => !fromState.includes(x));

    update(editId, {
      added,
      deleted
    });

    this.setState({ hasChanges: false });
  };

  cancelChanges = () => {
    this.setState((state, props) => ({
      currentValues: props.currentValues,
      hasChanges: false
    }));
  };

  render() {
    const { currentValues, hasChanges } = this.state;
    const { label, availableValues, loading } = this.props;

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
          onChange={this.handleChange}
          input={<Input />}
          renderValue={selected => (
            <div style={{ display: 'flex', flexWrap: 'wrap' }}>
              {selected.map(x => (
                <Chip
                  key={x.id}
                  label={x.name}
                  style={{ margin: '2px' }}
                  onDelete={() => this.handleDelete(x.id)}
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
            <SaveBtn onExecute={this.saveChanges} />
            <CancelBtn onExecute={this.cancelChanges} />
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
  }
}
