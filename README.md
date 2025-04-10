# demo-data-mapping-agent

## Configuration

## Azure OpenAI Service Configuration
Configuration for Azure OpenAI service integration:

- **Endpoint:** `https://rutzsco-demo-openai.openai.azure.com/`
- **API Key:** (Use your provided key securely during development)
- **ChatGPT Deployment:** `gpt-4o`

Ensure the Azure OpenAI service is properly provisioned and the deployment `gpt-4o` exists within your Azure environment.

## File and Path Configuration
Paths to essential JSON, XSL, and CSV files used by the application:

- **Data Product Template:** `C:\\Sample_shot\\Context\\DataProduct_ASN_Template.json`
- **Transformation File:** `C:\\Sample_shot\\Context\\Xform_CustomCreateAdvanceShipmentNoticeSAPABMReqMsg_to_CustomCreateAdvanceShipmentNoticeSAPEBMReqMsg.xsl`
- **Data Product Attribute Definitions:** `C:\\Sample_shot\\Context\\DeliveryDocument_DataProduct_AttributeDefinition_InProgress.csv`
- **Data Product Mappings:** `C:\\Sample_shot\\Context\\asn_mapping.json`
- **Input File:** `C:\\Sample_shot\\User_Input\\JSON_input_SAP_Delivery.json`

## Example Configuration File
Here is a basic example of the JSON file structure you can use as a starting point:

```json
{
  "AOAIStandardServiceEndpoint": "https://your-openai-endpoint.openai.azure.com/",
  "AOAIStandardServiceKey": "your-azure-openai-service-key",
  "AOAIStandardChatGptDeployment": "gpt-deployment-name",

  "DataProductTemplate": "C:\\path\\to\\DataProduct_ASN_Template.json",
  "TransformationFile": "C:\\path\\to\\transformation.xsl",
  "DataProductAttributeDefinitions": "C:\\path\\to\\attribute_definitions.csv",
  "DataProductMappings": "C:\\path\\to\\mappings.json",
  "InputFile": "C:\\path\\to\\input.json"
}
```

Replace the placeholders with your actual configuration details.

