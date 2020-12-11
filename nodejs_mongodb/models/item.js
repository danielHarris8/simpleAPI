const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const ItemSchema = new Schema({
  temperature: {
    type: Number,
    required: true
  },
  humidity: {
    type: Number,
    required: true
  },
  date: {
    type: Date,
    default: Date.now
  }
});

module.exports = Item = mongoose.model('item', ItemSchema);