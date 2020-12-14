FROM mongo:4.4.2-bionic

COPY base_data.json /base_data.json
CMD mongoimport --host mongodb --db effectory --collection questionnaire --mode upsert --type json --file /base_data.json --jsonArray
