

curl --location --request POST 'https://localhost:7233/api/expression/EvaluateOrderCsharpTree' \
--header 'Content-Type: application/json' \
--data-raw '{
```json
    "expressionDefinitions": [
        {
            "expression": "int age = 16; bool isExsitingCustomer = true; string output; decimal limit = 1000;"
        },
        {
            "expression": "isExsitingCustomer"
        }
    ],
    "imports": [],
    "leftNode": {
        "expressionDefinitions": [
            {
                "expression": "age > 18"
            }
        ],
        "imports": [],
        "leftNode": {
            "expressionDefinitions": [
                {
                    "expression": "output = limit > 1000 ? output = \"Gold\" : output = \"Classic\";"
                },
                {
                    "expression": "output"
                }
            ],
            "imports": [],
            "isLastNode": true
        },
        "rightNode": {
            "expressionDefinitions": [
                {
                    "expression": "output = \"Kid Card\";"
                },
                {
                    "expression": "output"
                }
            ],
            "imports": [],
            "isLastNode": true
        }
    },
    "rightNode": {
        "expressionDefinitions": [
            {
                "expression": "output = \"Be customer!!\";"
            },
            {
                "expression": "output"
            }
        ],
        "imports": [],
        "isLastNode": true
    }
```
}'


