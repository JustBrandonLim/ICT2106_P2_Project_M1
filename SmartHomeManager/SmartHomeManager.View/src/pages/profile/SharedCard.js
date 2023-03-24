import React, { useRef } from "react";
import {
  Card,
  CardHeader,
  CardBody,
  CardFooter,
  Stack,
  Heading,
  Text,
  Button,
  Image,
  Popover,
  PopoverTrigger,
  PopoverContent,
  PopoverHeader,
  PopoverBody,
  PopoverFooter,
  PopoverArrow,
  PopoverCloseButton,
  PopoverAnchor,
} from "@chakra-ui/react";
import { Link, useNavigate } from "react-router-dom";
import { css } from "@emotion/react";

function SharedProfileCard({ ProfileName, SName, SId, ProfilePic, Rules, IA }) {
  const textAreaRef = useRef(null);

  const handleCopyClick = () => {
    if (textAreaRef.current) {
      const text = textAreaRef.current.textContent;
      navigator.clipboard.writeText(text).then(() => {
        console.log("Copied to clipboard:", text);
      });
    }
  };

  return (
    <Card direction={{ base: "column", sm: "row" }} variant="outline">
      <Image
        objectFit="contain"
        maxW={{ base: "100%", sm: "200px" }}
        src={ProfilePic}
        alt="Profile Pic"
      />

      <Stack>
        <CardBody>
          <Heading size="md">{ProfileName} Profile</Heading>

          <Text>Scenario Name: {SName}</Text>
          <Text>Scenario Id: {SId}</Text>
          <Text>is Active: {IA}</Text>
        </CardBody>
        <CardFooter>
          <Popover>
            <PopoverTrigger>
              <Button variant="solid" colorScheme="green">
                More Details
              </Button>
            </PopoverTrigger>
            <PopoverContent color="white" bg="blue.700" borderColor="blue.800">
              <PopoverArrow />
              <PopoverCloseButton />
              <PopoverHeader fontWeight="bold">Scenario Rules</PopoverHeader>
              <PopoverBody ref={textAreaRef}>{Rules}</PopoverBody>
              <PopoverFooter>
                <Button
                  marginLeft="10px"
                  variant="solid"
                  colorScheme="blue"
                  onClick={handleCopyClick}
                >
                  Copy to Clipboard
                </Button>
              </PopoverFooter>
            </PopoverContent>
          </Popover>
        </CardFooter>
      </Stack>
    </Card>
  );
}

export function SharedProfileCardComponent({
  ProfileName,
  SName,
  SId,
  ProfilePic,
  Rules,
  IA,
}) {
  return (
    <SharedProfileCard
      IA={IA}
      Rules={Rules}
      ProfileName={ProfileName}
      SName={SName}
      SId={SId}
      ProfilePic={ProfilePic}
    />
  );
}
